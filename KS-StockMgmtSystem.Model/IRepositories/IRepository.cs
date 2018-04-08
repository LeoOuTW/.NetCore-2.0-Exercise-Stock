using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace KS_StockMgmtSystem.Model.IRepositories
{
    public interface IRepository
    {
    }

    //定義泛型倉儲接口
    public interface IRepository<TEntity, in TPrimaryKey> : IRepository where TEntity : Entity<TPrimaryKey>
    {
        void Delete(TEntity entity);
        void Delete(TPrimaryKey id);

        void DeleteRange(IEnumerable<TEntity> idList);

        void DeleteRange(IEnumerable<TPrimaryKey> idList);

        TEntity Get(TPrimaryKey id);
        Task<TEntity> GetAsync(TPrimaryKey id);
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);
        TEntity Insert(TEntity entity);
        TEntity InsertOrUpdate(TEntity entity);
        int Save();
        Task<int> SaveAsync();
        TEntity Update(TEntity entity);
    }

    //默認stirng主鍵類型倉儲
    public interface IRepository<TEntity> : IRepository<TEntity, string> where TEntity : Entity
    {
    }


    //默認Int主鍵類型倉儲
    public interface IRepositoryInt<TEntity> : IRepository<TEntity, int> where TEntity : EntityInt
    {
        IQueryable<TEntity> GetAllBySearchKey(SearchKey searchKey);
    }

    public interface ISearchRepository<TEntity> : IRepository where TEntity : EntityInt
    {

    }
}
