using Oren.Core.Core.Entity;
using Oren.Core.Core.Service;
using Oren.DAL.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;
using Oren.Model.Model.Entities;

namespace Oren.Service.BaseService
{
    public class BaseService<T> : ICoreService<T> where T : CoreEntity
    {
        private ProjectContext db;

        public BaseService()
        {
            db = new ProjectContext();

        }
        public void Add(T item)
        {
            db.Set<T>().Add(item);
            Save();
        }

        public bool Any(Expression<Func<T, bool>> exp) => db.Set<T>().Any(exp);
       

        public void Delete(T item)
        {
            T data = item;
            db.Set<T>().Remove(data);
            Save();
        }

        public void DeleteById(int id)
        {
            T data= GetById(id);

            db.Set<T>().Remove(data);
            db.SaveChanges();
        }

        public void DeleteAll(List<T> msg)
        {
            foreach (var item in msg)
            {
                Delete(item);
            }


        }



        public List<T> GetAll() => db.Set<T>().ToList();


        public List<T> GetByExp(Expression<Func<T, bool>> exp) => db.Set<T>().Where(exp).ToList();
        public T GetByExpSingle(Expression<Func<T, bool>> exp) => db.Set<T>().Where(exp).FirstOrDefault();

        public T GetById(int id)
        {
            T data = db.Set<T>().Find(id);
            return data;
        }

        public int Save() => db.SaveChanges();
       

        public void Update(T item)
        {
            T data = GetById(item.ID);
            DbEntityEntry entry = db.Entry(data);
            entry.CurrentValues.SetValues(item);
            Save();
        }

       

    }
}
