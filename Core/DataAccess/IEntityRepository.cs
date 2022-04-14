
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess
{
    //Generic constraint
    //class : referans tip 
    //IEntity : IEntity olabilir veya IEntity implemente bir nesne olabilir
    //new() : new'lenebilir olmalı(IEntity yazılamasın istiyoruz)
    public interface IEntityRepository<T> where T:class,IEntity,new()
    {
        //delege
        List<T> GetAll(Expression<Func<T,bool>> filter=null);  //filtre verebilirsin vermeyedebilirsin yani defaultu null. Mantık : Filtre vermezse tüm datayı getir; filtre verirse de filtreleyip getir
        T Get(Expression<Func<T, bool>> filter);  //Tek bir datayı getirmek; Örn,Bir bankacılık sisteminde birçok hesabımız var ve biz 1 tanesini seçip o hesabın detayına gitmek için kullanabiliriz 

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);

       
    }
}
