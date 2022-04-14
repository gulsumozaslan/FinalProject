using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    
    public interface IProductService
    {
        IDataResult<List<Product>> GetAll();
        IDataResult<List<Product>> GetAllByCategoryId(int id);  //Kategoriye göre filtreleme
        IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max);  //Fiyat aralığına göre filtreleme
        IDataResult<List<ProductDetailDto>> GetProductDetails();
        IDataResult<Product> GetById(int productId); 
        IResult Add(Product product);
        IResult Update(Product product);
        IResult TransactionalTest(Product product);

    }
}
