using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SaleShop.Model.Models;
using SaleShop.Web.Models;

namespace SaleShop.Web.Infrastructure.Extensions
{
    public static class EntityExtensions
    {
        //Extension Method
        //+ Phương thức và class phải static
        //+ Class sử dụng phải using namespace này
        //+ Tham số có chữ this chỉ định extension cho lớp đó VD : PostCategory ở dưới
        public static void UpdatePostCategory(this PostCategory postCategory, PostCategoryViewModel postCategoryViewModel)
        {
            postCategory.ID = postCategoryViewModel.ID;
            postCategory.Name = postCategoryViewModel.Name;
            postCategory.Alias = postCategoryViewModel.Alias;
            postCategory.Description = postCategoryViewModel.Description;
            postCategory.DisplayOrder = postCategoryViewModel.DisplayOrder;
            postCategory.HomeFlag = postCategoryViewModel.HomeFlag;
            postCategory.Image = postCategoryViewModel.Image;
            postCategory.ParentID = postCategoryViewModel.ParentID;

            postCategory.Status = postCategoryViewModel.Status;
            postCategory.CreatedBy = postCategoryViewModel.CreatedBy;
            postCategory.CreatedDate = postCategoryViewModel.CreatedDate;
            postCategory.MetaDescription = postCategoryViewModel.MetaDescription;
            postCategory.MetaKeyword = postCategoryViewModel.MetaKeyword;
            postCategory.UpdatedBy = postCategoryViewModel.UpdatedBy;
            postCategory.UpdatedDate = postCategoryViewModel.UpdatedDate;
        }
        public static void UpdateProductCategory(this ProductCategory productCategory, ProductCategoryViewModel productCategoryViewModel)
        {
            productCategory.ID = productCategoryViewModel.ID;
            productCategory.Name = productCategoryViewModel.Name;
            productCategory.Alias = productCategoryViewModel.Alias;
            productCategory.Description = productCategoryViewModel.Description;
            productCategory.DisplayOrder = productCategoryViewModel.DisplayOrder;
            productCategory.HomeFlag = productCategoryViewModel.HomeFlag;
            productCategory.Image = productCategoryViewModel.Image;
            productCategory.ParentID = productCategoryViewModel.ParentID;

            productCategory.Status = productCategoryViewModel.Status;
            productCategory.CreatedBy = productCategoryViewModel.CreatedBy;
            productCategory.CreatedDate = productCategoryViewModel.CreatedDate;
            productCategory.MetaDescription = productCategoryViewModel.MetaDescription;
            productCategory.MetaKeyword = productCategoryViewModel.MetaKeyword;
            productCategory.UpdatedBy = productCategoryViewModel.UpdatedBy;
            productCategory.UpdatedDate = productCategoryViewModel.UpdatedDate;
        }
        public static void UpdatePost(this Post post, PostViewModel postViewModel)
        {
            post.ID = postViewModel.ID;
            post.Name = postViewModel.Name;
            post.Alias = postViewModel.Alias;
            post.Description = postViewModel.Description;
            post.HomeFlag = postViewModel.HomeFlag;
            post.Image = postViewModel.Image;
            post.CategoryID = postViewModel.CategoryID;
            post.Content = postViewModel.Content;
            post.HotFlag = postViewModel.HotFlag;
            post.ViewCount = postViewModel.ViewCount;

            post.Status = postViewModel.Status;
            post.CreatedBy = postViewModel.CreatedBy;
            post.CreatedDate = postViewModel.CreatedDate;
            post.MetaDescription = postViewModel.MetaDescription;
            post.MetaKeyword = postViewModel.MetaKeyword;
            post.UpdatedBy = postViewModel.UpdatedBy;
            post.UpdatedDate = postViewModel.UpdatedDate;
        }

        public static void UpdateProduct(this Product product, ProductViewModel productViewModel)
        {
            product.ID = productViewModel.ID;
            product.Name = productViewModel.Name;
            product.Alias = productViewModel.Alias;
            product.Image = productViewModel.Image;
            product.CategoryID = productViewModel.CategoryID;
            product.Image = productViewModel.Image;
            product.MoreImages = productViewModel.MoreImages;
            product.Price = productViewModel.Price;
            product.PromotionPrice = productViewModel.PromotionPrice;
            product.Warranty = productViewModel.Warranty;
            product.Description = productViewModel.Description;
            product.Content = productViewModel.Content;
            product.HomeFlag = productViewModel.HomeFlag;
            product.HotFlag = productViewModel.HotFlag;
            product.ViewCount = productViewModel.ViewCount;

            product.Status = productViewModel.Status;
            product.CreatedBy = productViewModel.CreatedBy;
            product.CreatedDate = productViewModel.CreatedDate;
            product.MetaDescription = productViewModel.MetaDescription;
            product.MetaKeyword = productViewModel.MetaKeyword;
            product.UpdatedBy = productViewModel.UpdatedBy;
            product.UpdatedDate = productViewModel.UpdatedDate;
        }

    }
}