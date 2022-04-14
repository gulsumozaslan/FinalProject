using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess.EntityFramework
{ //Entity Framework kullanarak bir repository base'i oluştur
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>  //Entity ve context(northwind context) tipi ver
        where TEntity : class, IEntity, new()
        where TContext : DbContext, new()
    {
        public void Add(TEntity entity)
        {
            //IDisposable pattern implementation of c#
            using (TContext context = new TContext())
            {
                var addedEntity = context.Entry(entity); //referansı yakala. Northwind contexte bağla bu entity'i,veri kaynağıyla ilişkilendir. 
                addedEntity.State = EntityState.Added;  //bu aslında eklenecek bir nesne, State entity'nin o an ki durumunu bildiren property
                context.SaveChanges();                 //Yapılan ekleme(insert) işlemi veritabanına yansır.SaveChanges işlemi sonrasında Entity’nin state durumu UnChanged olarak değişecektir.

            }
        }

        public void Delete(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (TContext context = new TContext())
            {
                return context.Set<TEntity>().SingleOrDefault(filter);
            }
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            using (TContext context = new TContext())
            {
                //Ternary Operator
                return filter == null ? context.Set<TEntity>().ToList() : context.Set<TEntity>().Where(filter).ToList(); // Filtre vermezse tüm datayı getir; filtre verirse de filtreleyip getir
                //ÖRN DbSet'teki Products tablosu ile çalışır ve veritabanındaki tüm tabloyu listeye çevirir(arka planda select*from Products çalıştırıyor)
            }
        }

        public void Update(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
