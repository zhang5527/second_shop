﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace second_shop.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class second_shopEntities : DbContext
    {
        public second_shopEntities()
            : base("name=second_shopEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<admin> admin { get; set; }
        public virtual DbSet<collection> collection { get; set; }
        public virtual DbSet<comment> comment { get; set; }
        public virtual DbSet<index_config> index_config { get; set; }
        public virtual DbSet<infor> infor { get; set; }
        public virtual DbSet<product> product { get; set; }
        public virtual DbSet<requirement> requirement { get; set; }
        public virtual DbSet<user_history> user_history { get; set; }
        public virtual DbSet<users> users { get; set; }
    }
}