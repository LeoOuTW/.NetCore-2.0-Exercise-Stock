using KS_StockMgmtSystem.Model.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KS_StockMgmtSystem.Model
{
    public abstract class Entity<TPrimaryKey>
    {
        [Key]
        public virtual TPrimaryKey Id { get; set; }
    }

    public abstract class Entity : Entity<string>
    {
        [Key]
        [MaxLength(100)]
        public override string Id { get; set; }
    }


    public abstract class EntityInt : Entity<int>
    {

    }

    //public abstract class EntityIntWithLang : EntityInt
    //{
    //    //父Object
    //    public int? ObjectId { get; set; }

    //    //語言Id
    //    public int? LangId { get; set; }
    //}

    public abstract class EntityIntWithRecord : EntityInt
    {
        private string _createUser;
        private string _updateUser;
        public DateTime? CreateDate { get; set; }

        [MaxLength(16)]
        public string CreateUser
        {
            get => _createUser ?? (_createUser = string.Empty);
            set => _createUser = value;
        }

        public DateTime? UpdateDate { get; set; }

        [MaxLength(16)]
        public string UpdateUser
        {
            get => _updateUser ?? (_updateUser = string.Empty);
            set => _updateUser = value;
        }

        ////-1: 已刪除，0: 停用，1: 啟用
        public Status Status { get; set; }
    }
}
