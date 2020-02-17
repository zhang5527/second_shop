using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Z.EntityFramework.Plus;

namespace second_shop
{

    public class BaseDal<T> where T : class, new()
    {
        public static T AddEntity(T eneity)
        {
            Instance.getSingal.Set<T>().Add(eneity);
            Instance.getSingal.SaveChanges();
            return eneity;
        }
        //改动 
        public IQueryable<T> loadAllEntities(Expression<Func<T, Object>> WhereLambda)
        {
            return Instance.getSingal.Set<T>().Select<T, Object>(WhereLambda) as IQueryable<T>;
        }

        public static T firstordefault(Expression<Func<T, bool>> WhereLambda)
        {
            return Instance.getSingal.Set<T>().FirstOrDefault(WhereLambda);
        }

        public static bool DeleteEntity(T eneity)
        {
            Instance.getSingal.Entry<T>(eneity).State = System.Data.Entity.EntityState.Deleted;
            Instance.getSingal.SaveChanges();
            return true;
        }


        public static int DeleteByIds(IEnumerable<T> ids)
        {
            return Instance.getSingal.Set<T>().DeleteRangeByKey(ids);
        }

        public static void UpdateMany(IEnumerable<T> entites)
        {
             Instance.getSingal.Set<T>().BulkUpdate(entites) ;
        }
        
        public static bool DeleteEntity(string sql)
        {
            Instance.getSingal.Database.ExecuteSqlCommand(sql);
            return true;
        }
        public static IQueryable<T> LoadEntities(System.Linq.Expressions.Expression<Func<T, bool>> WhereLambda)
        {
            return Instance.getSingal.Set<T>().Where<T>(WhereLambda);
        }

        public static IQueryable<T> LoadEntitiesByPages<s>(int pageindex, int pagesize, out int TotalCount, System.Linq.Expressions.Expression<Func<T, bool>> WhereLambda, System.Linq.Expressions.Expression<Func<T, s>> OrderByLambda, bool isasy)
        {
            var temp = Instance.getSingal.Set<T>().Where<T>(WhereLambda);
            TotalCount = temp.Count();
            if (isasy)
            {
                temp = temp.OrderBy<T, s>(OrderByLambda).Skip<T>((pageindex - 1) * pagesize).Take<T>(pagesize);
            }
            else
            {
                temp = temp.OrderByDescending<T, s>(OrderByLambda).Skip<T>((pageindex - 1) * pagesize).Take<T>(pagesize);
            }
            return temp;
        }

        public static bool ModifyEntity(T eneity)
        {
            Instance.getSingal.Entry<T>(eneity).State = EntityState.Modified;
            if (Instance.getSingal.SaveChanges() >= 1)
                return true;
            return false;
        }

    }
}