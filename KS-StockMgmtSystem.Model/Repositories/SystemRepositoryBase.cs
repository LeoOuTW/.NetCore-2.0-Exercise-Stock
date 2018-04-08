using KS_StockMgmtSystem.Model.Entities;
using KS_StockMgmtSystem.Model.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace KS_StockMgmtSystem.Model.Repositories
{
    /// <summary>
    /// 基礎類型
    /// </summary>
    public abstract class SystemRepositoryBase<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey>
        where TEntity : Entity<TPrimaryKey>
    {
        //定義數據庫訪問上下對象
        private readonly SystemDBContext _dbContext;

        protected SystemRepositoryBase(SystemDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// 獲取實體集合
        /// </summary>
        /// <returns></returns>
        public IQueryable<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>().Where(CreateBaseFilter());
        }

        /// <summary>
        /// 根據lambda表達式條件獲取實體集合
        /// </summary>
        /// <param name="predicate">lambda表達式條件</param>
        /// <returns></returns>
        public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbContext.Set<TEntity>().Where(CreateBaseFilter()).Where(predicate);
        }

        /// <summary>
        /// 根據主鍵獲取實體
        /// </summary>
        /// <param name="id">實體主鍵</param>
        /// <returns></returns>
        public TEntity Get(TPrimaryKey id)
        {
            return _dbContext.Set<TEntity>().Where(CreateBaseFilter())
                .FirstOrDefault(CreateEqualityExpressionForId(id));
        }

        public Task<TEntity> GetAsync(TPrimaryKey id)
        {
            return _dbContext.Set<TEntity>().Where(CreateBaseFilter())
                .FirstOrDefaultAsync(CreateEqualityExpressionForId(id));
        }

        /// <summary>
        /// 新增實體
        /// </summary>
        /// <param name="entity">實體</param>
        /// <returns></returns>
        public TEntity Insert(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
            return entity;
        }

        /// <summary>
        /// 更新實體
        /// </summary>
        /// <param name="entity">實體</param>
        public TEntity Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            return entity;
        }


        /// <summary>
        /// 新增或更新實體
        /// </summary>
        /// <param name="entity">實體</param>
        public TEntity InsertOrUpdate(TEntity entity)
        {
            return Get(entity.Id) == null ? Insert(entity) : Update(entity);
        }

        /// <summary>
        /// 刪除實體
        /// </summary>
        /// <param name="entity">要刪除的實體</param>
        public void Delete(TEntity entity)
        {
            //軟刪除
            var mapper = TrySetProperty(entity, "Status", -1);
            if (mapper)
            {
                _dbContext.Set<TEntity>().Update(entity);
            }
            //若無法軟刪除，實行真刪除
            else
            {
                _dbContext.Set<TEntity>().Remove(entity);
            }
        }

        private bool TrySetProperty(object obj, string property, object value)
        {
            var prop = obj.GetType().GetProperty(property, BindingFlags.Public | BindingFlags.Instance);
            if (prop == null || !prop.CanWrite) return false;

            prop.SetValue(obj, value, null);
            return true;
        }

        /// <summary>
        /// 刪除實體
        /// </summary>
        /// <param name="id">實體主鍵</param>
        public void Delete(TPrimaryKey id)
        {
            var getData = Get(id);
            if (getData != null)
            {
                Delete(getData);
            }
        }

        /// <summary>
        /// 刪除實體
        /// </summary>
        /// <param name="id">實體主鍵</param>
        public void DeleteRange(IEnumerable<TPrimaryKey> id)
        {
            foreach (var i in id)
            {
                Delete(i);
            }
        }


        /// <summary>
        /// 刪除實體
        /// </summary>
        /// <param name="id">實體主鍵</param>
        public void DeleteRange(IEnumerable<TEntity> id)
        {
            foreach (var i in id)
            {
                Delete(i);
            }
        }

        /// <summary>
        /// 事務性保存
        /// </summary>
        public int Save()
        {
            return _dbContext.SaveChanges();
        }

        public async Task<int> SaveAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// 根據主鍵構建判斷表達式
        /// </summary>
        /// <param name="id">主鍵</param>
        /// <returns></returns>
        private static Expression<Func<TEntity, bool>> CreateEqualityExpressionForId(TPrimaryKey id)
        {
            var lambdaParam = Expression.Parameter(typeof(TEntity));
            var lambdaBody = Expression.Equal(
                Expression.PropertyOrField(lambdaParam, "Id"),
                Expression.Constant(id, typeof(TPrimaryKey))
            );

            return Expression.Lambda<Func<TEntity, bool>>(lambdaBody, lambdaParam);
        }

        private static Expression<Func<TEntity, bool>> CreateBaseFilter()
        {
            var checkParams = typeof(TEntity).GetProperty("Status");
            if (checkParams == null)
            {
                return o => true;
            }

            var lambdaParam = Expression.Parameter(typeof(TEntity));
            var comparedEntityParam = Expression.PropertyOrField(lambdaParam, "Status");
            var enumType = typeof(Status);
            var enabledStatus = Expression.Constant(Status.Disabled, enumType);
            var lambdaBody =
                Expression.GreaterThanOrEqual(
                    Expression.Convert(comparedEntityParam, typeof(int)),
                    Expression.Convert(enabledStatus, typeof(int))
                );

            return Expression.Lambda<Func<TEntity, bool>>(lambdaBody, lambdaParam);
        }
    }

    public abstract class SystemRepositoryBase<TEntity> : SystemRepositoryBase<TEntity, string>
        where TEntity : Entity
    {
        public SystemRepositoryBase(SystemDBContext dbContext) : base(dbContext)
        {
        }
    }


    public abstract class SystemRepositoryBaseInt<TEntity> : SystemRepositoryBase<TEntity, int>
        where TEntity : EntityInt
    {
        public SystemRepositoryBaseInt(SystemDBContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<TEntity> GetAllBySearchKey(SearchKey searchKey)
        {
            var raw = GetAll();
            var properties = typeof(SearchKey).GetProperties();
            var bannerType = typeof(TEntity);
            var lambdaParam = Expression.Parameter(bannerType);
            foreach (var property in properties)
            {
                var name = property.Name;
                var checkParams = bannerType.GetProperty(name);
                if (checkParams == null)
                {
                    continue;
                }

                var value = property.GetValue(searchKey);
                if (value == null)
                {
                    continue;
                }

                var comparedEntityParam = Expression.PropertyOrField(lambdaParam, name);
                if (property.PropertyType == typeof(DateTime?))
                {
                    var targetType = property.PropertyType;
                    var targetDateTime = Expression.Constant(value, targetType);
                    BinaryExpression lambdaBody = null;
                    if (name.ToLower().Contains("start"))
                    {
                        lambdaBody = Expression.LessThanOrEqual(comparedEntityParam, targetDateTime);
                    }
                    else if (name.ToLower().Contains("end"))
                    {
                        lambdaBody = Expression.GreaterThanOrEqual(comparedEntityParam, targetDateTime);
                    }

                    if (lambdaBody != null)
                    {
                        raw = raw.Where(Expression.Lambda<Func<TEntity, bool>>(lambdaBody, lambdaParam));
                    }
                }

                if (property.PropertyType == typeof(Status?))
                {
                    var enumType = typeof(Status);
                    var enabledStatus = Expression.Constant(value, enumType);
                    var lambdaBody =
                        Expression.Equal(
                            Expression.Convert(comparedEntityParam, typeof(int)),
                            Expression.Convert(enabledStatus, typeof(int))
                        );
                    raw = raw.Where(Expression.Lambda<Func<TEntity, bool>>(lambdaBody, lambdaParam));
                }

                if (property.PropertyType == typeof(string))
                {
                    var method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                    var targetValue = Expression.Constant(value, typeof(string));
                    var exp = Expression.Call(comparedEntityParam, method, targetValue);
                    raw = raw.Where(Expression.Lambda<Func<TEntity, bool>>(exp, lambdaParam));
                }
            }

            return raw.AsNoTracking();
        }
    }
}
