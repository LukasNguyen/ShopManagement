﻿using SaleShop.Data.Infrastructure;
using SaleShop.Data.Repositories;
using SaleShop.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using SaleShop.Common;

namespace SaleShop.Service
{
    public interface IProductService
    {
        Product Add(Product product);

        void Update(Product product);

        Product Delete(int id);

        IEnumerable<Product> GetAll();

        IEnumerable<Product> GetAll(string keyword);

        IEnumerable<Product> GetLastest(int top);
        IEnumerable<Product> GetHotProduct(int top);

        IEnumerable<Product> GetListProductByCategoryPaging(int categoryId,int page,int pageSize,string sort,out int totalRow);

        IEnumerable<string> GetListProductByName(string name);

        IEnumerable<Product> Search(string keyword, int page, int pageSize, string sort, out int totalRow);

        Product GetById(int id);

        void Save();
    }

    public class ProductService : IProductService
    {
        private IProductRepository _productRepository;
        private IUnitOfWork _unitOfWork;

        //Apply Tag to product
        private ITagRepository _tagRepository;

        private IProductTagRepository _productTagRepository;

        public ProductService(IProductRepository productRepository,IProductTagRepository productTagRepository,ITagRepository tagRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;

            _tagRepository = tagRepository;
            _productTagRepository = productTagRepository;
        }

        public Product Add(Product product)
        {
            var newProduct = _productRepository.Add(product);
            //_unitOfWork.Commit();

            if (!String.IsNullOrEmpty(product.Tags))
            {
                string[] tags = product.Tags.Split(',');

                for (int i = 0; i < tags.Length; i++)
                {
                    var tagID = StringHelper.ToUnsignString(tags[i]);

                    //Nếu cái tag nào chưa có thì tạo mới
                    if (_tagRepository.Count(n => n.ID == tagID) == 0)
                    {
                        Tag tag = new Tag();
                        tag.ID = tagID;
                        tag.Name = tags[i];
                        tag.Type = CommonConstants.ProductTag;

                        _tagRepository.Add(tag);
                    }
                    
                    ProductTag productTag = new ProductTag();
                    productTag.ProductID = product.ID;
                    productTag.TagID = tagID;

                    _productTagRepository.Add(productTag);
                }
            }

            return newProduct;
        }

        public void Update(Product product)
        {
            _productRepository.Update(product);

            if (!String.IsNullOrEmpty(product.Tags))
            {
                string[] tags = product.Tags.Split(',');

                for (int i = 0; i < tags.Length; i++)
                {
                    var tagID = StringHelper.ToUnsignString(tags[i]);

                    //Nếu cái tag nào chưa có thì tạo mới
                    if (_tagRepository.Count(n => n.ID == tagID) == 0)
                    {
                        Tag tag = new Tag();
                        tag.ID = tagID;
                        tag.Name = tags[i];
                        tag.Type = CommonConstants.ProductTag;

                        _tagRepository.Add(tag);
                    } 
                    _productTagRepository.DeleteMulti(n=>n.ProductID == product.ID);

                    ProductTag productTag = new ProductTag();
                    productTag.ProductID = product.ID;
                    productTag.TagID = tagID;

                    _productTagRepository.Add(productTag);
                } 
            }

        }

        public Product Delete(int id)
        {
            return _productRepository.Delete(id);
        }

        public IEnumerable<Product> GetAll()
        {
            return _productRepository.GetAll();
        }

        public IEnumerable<Product> GetAll(string keyword)
        {
            if (!String.IsNullOrEmpty(keyword))
                return _productRepository.GetMulti(
                    n => n.Name.Contains(keyword) || n.Description.Contains(keyword));
            else
                return _productRepository.GetAll();
        }

        public Product GetById(int id)
        {
            return _productRepository.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public IEnumerable<Product> GetLastest(int top)
        {
            return _productRepository.GetMulti(n => n.Status).OrderByDescending(n => n.CreatedDate).Take(top);
        }

        public IEnumerable<Product> GetHotProduct(int top)
        {
            return _productRepository.GetMulti(n => n.Status && n.HotFlag == true).OrderByDescending(n => n.CreatedDate).Take(top);
        }

        public IEnumerable<Product> GetListProductByCategoryPaging(int categoryId, int page, int pageSize,string sort, out int totalRow)
        {
            var query = _productRepository.GetMulti(n => n.Status && n.CategoryID == categoryId);

            switch (sort)
            {

                case "popular":
                    query = query.OrderByDescending(n => n.ViewCount);
                    break;
                case "discount":
                    query = query.OrderByDescending(n => n.PromotionPrice.HasValue);
                    break;
                case "price":
                    query = query.OrderBy(n => n.Price);
                    break;
                default:
                    query = query.OrderByDescending(n => n.CreatedDate);
                    break;
            }

            totalRow = query.Count();

            return query.Skip(pageSize * (page-1)).Take(pageSize);    
        }

        public IEnumerable<string> GetListProductByName(string name)
        {
            return _productRepository.GetMulti(n => n.Status && n.Name.Contains(name)).Select(n=>n.Name);
        }

        public IEnumerable<Product> Search(string keyword, int page, int pageSize, string sort, out int totalRow)
        {
            var query = _productRepository.GetMulti(n => n.Status && n.Name.Contains(keyword));

            switch (sort)
            {

                case "popular":
                    query = query.OrderByDescending(n => n.ViewCount);
                    break;
                case "discount":
                    query = query.OrderByDescending(n => n.PromotionPrice.HasValue);
                    break;
                case "price":
                    query = query.OrderBy(n => n.Price);
                    break;
                default:
                    query = query.OrderByDescending(n => n.CreatedDate);
                    break;
            }

            totalRow = query.Count();

            return query.Skip(pageSize * (page - 1)).Take(pageSize);
        }
    }
}