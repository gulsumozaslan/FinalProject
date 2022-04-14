using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IProductDal : IEntityRepository<Product>
    {
        //ürüne ait özel operasyonları koyucaz(örn ürünün detaylarını getirmek için ürünün kategori tablolarına join atmak gibi)
        List<ProductDetailDto> GetProductDetails();  //Ürünün detaylarını getir

    }
}
//interface metotları default publictir; kendisi değil
