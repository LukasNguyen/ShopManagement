using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SaleShop.Data.Infrastructure;
using SaleShop.Data.Repositories;
using SaleShop.Model.Models;
using SaleShop.Service;

namespace SaleShop.UnitTest.ServiceTest
{
    [TestClass]
    public class PostCategoryServiceTest
    {
        private Mock<IPostCategoryRepository> _mockRepository;
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private IPostCategoryService _categoryService;
        private List<PostCategory> _listCategory;

        [TestInitialize]
        public void Initialize()
        {
            _mockRepository = new Mock<IPostCategoryRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _categoryService = new PostCategoryService(_mockRepository.Object,_mockUnitOfWork.Object);
            _listCategory = new List<PostCategory>()
            {
                new PostCategory(){ ID = 1,Name = "DM1",Status = true},
                new PostCategory(){ ID = 2,Name = "DM2",Status = true},
                new PostCategory(){ ID = 3,Name = "DM3",Status = true}
            };
        }
        [TestMethod]
        public void PostCategory_Service_GetAll()
        {
            //Setup Method
            _mockRepository.Setup(n => n.GetAll(null)).Returns(_listCategory);

            //Get All
            var result = _categoryService.GetAll() as List<PostCategory>;

            //Compare
            Assert.IsNotNull(result);
            Assert.AreEqual(3,result.Count);
        }
        [TestMethod]
        public void PostCategory_Service_Create()
        {
            PostCategory postCategory = new PostCategory();
            postCategory.Name = "Test Category";
            postCategory.Alias = "Test-Category";
            postCategory.Status = true;

            _mockRepository.Setup(n => n.Add(postCategory)).Returns((PostCategory p) =>
            {
                p.ID = 1; // setup 1 phương thức add trả về id = 1
                return p;
            });

            var result = _categoryService.Add(postCategory);

            Assert.IsNotNull(result);
            Assert.AreEqual(1,result.ID);

        }
    }
}
