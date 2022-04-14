 using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;  //Dependes injection
        ICategoryService _categoryService;
        

        public ProductManager(IProductDal productDal,ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;
            
        }
         
        //Claim
        [SecuredOperation("product.add,admin")]
        [ValidationAspect(typeof(ProductValidator))]   //Add metodunu doğrula ProductValidatordaki kurallara göre
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Add(Product product)
        {

            IResult result = BusinessRules.Run(CheckIfProductNameExists(product.ProductName),CheckIfProductCountOfCategoryCorrect(product.CategoryId),CheckIfCategoryLimitedExceded());  
            
            if (result != null)
            {
                return result;
            } 
            _productDal.Add(product); //Ürün eklendi

            return new SuccessResult(Messages.ProductAdded);  //Kullanıcı bilgilendirme


        }
        [CacheAspect]  //key,value
        public IDataResult<List<Product>> GetAll()
        {
            //İş kodları
            //Yetkisi var mı?
            //if-elseler

            if (DateTime.Now.Hour == 3) //örn şu anki saatim 3 olduğunda sistemi kapatmak istiyoruz ürünler listelenmesin
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime); //bakım zamanı
            }
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(),Messages.ProductsListed); //İş kodlarını geçerse Data Access'e bana ürünleri verebilirsin diyor

        }

        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId == id));
        }

        [CacheAspect]
        //[PerformanceAspect(5)]   //Bu metodun çalışması 5 sn'yi geçerse beni uyar (yani sistemde bir yavaşlık var)
        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(P => P.ProductId == productId));

        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max));
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails());
        }

        [ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IProductService.GetAll")]  
        public IResult Update(Product product)
        {
            var result = _productDal.GetAll(p => p.CategoryId == product.CategoryId).Count;
            if (result >= 10)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);
            }
            throw new NotImplementedException();
        }

        private IResult CheckIfProductCountOfCategoryCorrect(int categoryId)  //Bir kategoride en fazla 15 ürün olabilir
        {
            
            var result = _productDal.GetAll(p => p.CategoryId == categoryId).Count;          
            if (result >= 15)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);
            }
            return new SuccessResult();
        }

        private IResult CheckIfProductNameExists(string productName)  //Aynı isimde ürün eklenemez
        {
            
            var result = _productDal.GetAll(p => p.ProductName == productName).Any();
            if (result)
            {
                return new ErrorResult(Messages.ProductNameAlreadyExists);
            }
            return new SuccessResult();
           
        }

        private IResult CheckIfCategoryLimitedExceded()   //Eğer mevcut kategori sayısı 15'i geçtiyse sisteme yeni ürün eklenemez
        {
            var result = _categoryService.GetAll();
            if (result.Data.Count>15)
            {
                return new ErrorResult(Messages.CategoryLimitExceded);
            }

            return new SuccessResult();
        }

        //[TransactionScopeAspect]
        public IResult TransactionalTest(Product product)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                
                Add(product);
                if (product.UnitPrice < 10)
                {
                    throw new Exception("");

                }
                Add(product); 
                return null;
                  
            }
            
           
        }
    }

}
