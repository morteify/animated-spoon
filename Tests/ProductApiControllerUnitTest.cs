using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Moq;
using animated_spoon.Models;
using animated_spoon.Controllers;

namespace ProductControllerUnitTests
{
    public class ProductControllerUnitTest
    {
        [Fact]
        public void Products_ReturnsProductsList_Ok()
        {
            // arrange
            var mock = new Mock<EFProductRepository>();
            var productCategoryMock = new Mock<ProductCategory>();
            var productsDTO = new ProductApiDTO[] {
                new ProductApiDTO { ProductId = 1, Name = "N1", Description = "D1", Price = 10, ProductCategoryName = "C1", ProductCategoryId = 1 },
                new ProductApiDTO { ProductId = 2, Name = "N2", Description = "D2", Price = 11, ProductCategoryName = "C1", ProductCategoryId = 1 },
                new ProductApiDTO { ProductId = 3, Name = "N3", Description = "D3", Price = 12, ProductCategoryName = "C1", ProductCategoryId = 1 },
                new ProductApiDTO { ProductId = 4, Name = "N4", Description = "D4", Price = 13, ProductCategoryName = "C1", ProductCategoryId = 1 },
                new ProductApiDTO { ProductId = 5, Name = "N5", Description = "D5", Price = 14, ProductCategoryName = "C1", ProductCategoryId = 1 },
            };
            var products = new Product[] {
                 new Product {ProductId = 1, Name = "N1", Description = "D1", Price = 10, ProductCategory = productCategoryMock.Object, ProductCategoryId = 1},
                 new Product {ProductId = 2, Name = "N2", Description = "D1", Price = 10, ProductCategory = productCategoryMock.Object, ProductCategoryId = 1},
                 new Product {ProductId = 3, Name = "N3", Description = "D1", Price = 10, ProductCategory = productCategoryMock.Object, ProductCategoryId = 1},
                 new Product {ProductId = 4, Name = "N4", Description = "D1", Price = 10, ProductCategory = productCategoryMock.Object, ProductCategoryId = 1},
                 new Product {ProductId = 5, Name = "N5", Description = "D1", Price = 10, ProductCategory = productCategoryMock.Object, ProductCategoryId = 1}
            };
            mock.Setup(repo => repo.GetProducts()).Returns(products.AsQueryable<Product>());
            productCategoryMock.Setup(repo => repo.Name).Returns("C1");
            productCategoryMock.Setup(repo => repo.ProductCategoryId).Returns(1);
            var controller = new ProductApiController(mock.Object);

            // act
            var result = controller.Products();

            // assert 
            mock.Verify();
            Assert.Equal(productsDTO[0].ProductId, result.Value[0].ProductId);
            Assert.Equal(productsDTO[0].Description, result.Value[0].Description);
            Assert.Equal(productsDTO[0].ProductCategoryName, result.Value[0].ProductCategoryName);
        }

        [Fact]
        public void Products_ReturnsProductsList_NotFound()
        {
            // arrange
            var mock = new Mock<EFProductRepository>();
            var productCategoryMock = new Mock<ProductCategory>();
            var controller = new ProductApiController(mock.Object);

            // act
            var result = controller.Products();

            // assert 
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void ProductItem_ReturnsProductsItemById_Ok()
        {
            // arrange
            var mock = new Mock<EFProductRepository>();
            var productCategoryMock = new Mock<ProductCategory>();

            var products = new Product[] {
                 new Product {ProductId = 1, Name = "N1", Description = "D1", Price = 10, ProductCategory = productCategoryMock.Object, ProductCategoryId = 1},
                 new Product {ProductId = 2, Name = "N2", Description = "D1", Price = 10, ProductCategory = productCategoryMock.Object, ProductCategoryId = 2},
                 new Product {ProductId = 3, Name = "N3", Description = "D1", Price = 10, ProductCategory = productCategoryMock.Object, ProductCategoryId = 3},
                 new Product {ProductId = 4, Name = "N4", Description = "D1", Price = 10, ProductCategory = productCategoryMock.Object, ProductCategoryId = 4},
                 new Product {ProductId = 5, Name = "N5", Description = "D1", Price = 10, ProductCategory = productCategoryMock.Object, ProductCategoryId = 1}
            };

            var productDTO = new ProductApiDTO()
            {
                ProductId = 1,
                Name = "N1",
                Description = "D1",
                Price = 10,
                ProductCategoryName = "C1",
                ProductCategoryId = 1

            };

            mock.Setup(repo => repo.GetProducts()).Returns(products.AsQueryable<Product>());
            productCategoryMock.Setup(repo => repo.Name).Returns("C1");
            productCategoryMock.Setup(repo => repo.ProductCategoryId).Returns(1);

            var controller = new ProductApiController(mock.Object);

            // act
            var result = controller.ProductItem(1);

            // assert 
            Assert.IsType<ActionResult<ProductApiDTO>>(result);
            Assert.Equal(productDTO.ProductCategoryId, result.Value.ProductCategoryId);
            Assert.Equal(productDTO.ProductCategoryName, result.Value.ProductCategoryName);
            Assert.Equal(productDTO.ProductId, result.Value.ProductId);
        }


        [Fact]
        public void ProductItem_ReturnsProductsItemById_NotFound()
        {
            // arrange
            var mock = new Mock<EFProductRepository>();
            var productCategoryMock = new Mock<ProductCategory>();

            var products = new Product[] {
                 new Product {ProductId = 1, Name = "N1", Description = "D1", Price = 10, ProductCategory = productCategoryMock.Object, ProductCategoryId = 1},
                 new Product {ProductId = 2, Name = "N2", Description = "D1", Price = 10, ProductCategory = productCategoryMock.Object, ProductCategoryId = 1},
                 new Product {ProductId = 3, Name = "N3", Description = "D1", Price = 10, ProductCategory = productCategoryMock.Object, ProductCategoryId = 1},
                 new Product {ProductId = 4, Name = "N4", Description = "D1", Price = 10, ProductCategory = productCategoryMock.Object, ProductCategoryId = 1},
                 new Product {ProductId = 5, Name = "N5", Description = "D1", Price = 10, ProductCategory = productCategoryMock.Object, ProductCategoryId = 1}
            };
            mock.Setup(repo => repo.GetProducts()).Returns(products.AsQueryable<Product>());

            var controller = new ProductApiController(mock.Object);

            // act
            var result = controller.ProductItem(1231312);

            // assert 
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void ProductsList_ReturnsProductsItemByCategory_Ok()
        {
            // arrange
            var mock = new Mock<EFProductRepository>();
            var productCategoryMock = new Mock<ProductCategory>();

            var productsDTO = new ProductApiDTO[] {
                new ProductApiDTO { ProductId = 1, Name = "N1", Description = "D1", Price = 10, ProductCategoryName = "C1", ProductCategoryId = 1 },
                new ProductApiDTO { ProductId = 3, Name = "N3", Description = "D3", Price = 12, ProductCategoryName = "C1", ProductCategoryId = 1 },
            };

            var products = new Product[] {
                 new Product {ProductId = 1, Name = "N1", Description = "D1", Price = 10, ProductCategory = productCategoryMock.Object, ProductCategoryId = 1},
                 new Product {ProductId = 2, Name = "N2", Description = "D1", Price = 10, ProductCategory = productCategoryMock.Object, ProductCategoryId = 2},
                 new Product {ProductId = 3, Name = "N3", Description = "D1", Price = 10, ProductCategory = productCategoryMock.Object, ProductCategoryId = 1},
                 new Product {ProductId = 4, Name = "N4", Description = "D1", Price = 10, ProductCategory = productCategoryMock.Object, ProductCategoryId = 3},
                 new Product {ProductId = 5, Name = "N5", Description = "D1", Price = 10, ProductCategory = productCategoryMock.Object, ProductCategoryId = 4}
            };
            mock.Setup(repo => repo.GetProducts()).Returns(products.AsQueryable<Product>());
            productCategoryMock.Setup(repo => repo.Name).Returns("C1");
            productCategoryMock.Setup(repo => repo.ProductCategoryId).Returns(1);
            var controller = new ProductApiController(mock.Object);

            // act
            var result = controller.ProductsList(1);

            // assert 
            Assert.Equal(result.Value.ToList().Count, 2);
            Assert.Equal(productsDTO[0].ProductCategoryId, result.Value[0].ProductCategoryId);
            Assert.Equal(productsDTO[1].ProductCategoryId, result.Value[1].ProductCategoryId);
            Assert.Equal(productsDTO[0].ProductCategoryName, result.Value[0].ProductCategoryName);
            Assert.Equal(productsDTO[1].ProductCategoryName, result.Value[1].ProductCategoryName);
            Assert.Equal(productsDTO[0].ProductId, result.Value[0].ProductId);
            Assert.Equal(productsDTO[1].ProductId, result.Value[1].ProductId);
        }

        [Fact]
        public void ProductsList_ReturnsProductsItemByCategory_NotFound()
        {
            // arrange
            var mock = new Mock<EFProductRepository>();
            var productCategoryMock = new Mock<ProductCategory>();

            var products = new Product[] {
                 new Product {ProductId = 1, Name = "N1", Description = "D1", Price = 10, ProductCategory = productCategoryMock.Object, ProductCategoryId = 1},
                 new Product {ProductId = 2, Name = "N2", Description = "D1", Price = 10, ProductCategory = productCategoryMock.Object, ProductCategoryId = 1},
                 new Product {ProductId = 3, Name = "N3", Description = "D1", Price = 10, ProductCategory = productCategoryMock.Object, ProductCategoryId = 1},
                 new Product {ProductId = 4, Name = "N4", Description = "D1", Price = 10, ProductCategory = productCategoryMock.Object, ProductCategoryId = 1},
                 new Product {ProductId = 5, Name = "N5", Description = "D1", Price = 10, ProductCategory = productCategoryMock.Object, ProductCategoryId = 1}
            };
            mock.Setup(repo => repo.GetProducts()).Returns(products.AsQueryable<Product>());

            var controller = new ProductApiController(mock.Object);

            // act
            var result = controller.ProductsList(123);

            // assert 
            Assert.IsType<NotFoundResult>(result.Result);
            Assert.Null(result.Value);
        }

        [Fact]
        public void AddProduct_InsertsNewProductIntoDatabase_Ok()
        {
            // arrange
            var mock = new Mock<EFProductRepository>();
            var productCategoryMock = new Mock<ProductCategory>();

            var products = new Product[] {
                 new Product {ProductId = 1, Name = "N1", Description = "D1", Price = 10, ProductCategory = productCategoryMock.Object, ProductCategoryId = 1},
                 new Product {ProductId = 2, Name = "N2", Description = "D1", Price = 10, ProductCategory = productCategoryMock.Object, ProductCategoryId = 1},
                 new Product {ProductId = 3, Name = "N3", Description = "D1", Price = 10, ProductCategory = productCategoryMock.Object, ProductCategoryId = 1},
                 new Product {ProductId = 4, Name = "N4", Description = "D1", Price = 10, ProductCategory = productCategoryMock.Object, ProductCategoryId = 1},
                 new Product {ProductId = 5, Name = "N5", Description = "D1", Price = 10, ProductCategory = productCategoryMock.Object, ProductCategoryId = 1}
            };
            Product productToInsert = new Product { ProductId = 66, Name = "N66", Description = "D6", Price = 66, ProductCategory = productCategoryMock.Object, ProductCategoryId = 66 };
            mock.Setup(repo => repo.SaveProduct(It.IsAny<Product>())).Returns(productToInsert);
            productCategoryMock.Setup(repo => repo.Name).Returns("R92");
            productCategoryMock.Setup(repo => repo.ProductCategoryId).Returns(192);
            var controller = new ProductApiController(mock.Object);

            // action
            var result = controller.AddProduct(productToInsert);

            // assert 
            Assert.Equal(66, result.Value.ProductId);
            Assert.Equal(66, result.Value.ProductCategoryId);
            Assert.Equal("R92", result.Value.ProductCategory.Name);
            Assert.Equal(192, result.Value.ProductCategory.ProductCategoryId);
        }

        [Fact]
        public void UpdateProduct_UpdatesExistingProductInDatabase_Ok()
        {
            // arrange
            var mock = new Mock<EFProductRepository>();
            var productCategoryMock = new Mock<ProductCategory>();

            var products = new Product[] {
                 new Product {ProductId = 1, Name = "N1", Description = "D1", Price = 10, ProductCategory = productCategoryMock.Object, ProductCategoryId = 1},
                 new Product {ProductId = 2, Name = "N2", Description = "D1", Price = 10, ProductCategory = productCategoryMock.Object, ProductCategoryId = 1},
                 new Product {ProductId = 3, Name = "N3", Description = "D1", Price = 10, ProductCategory = productCategoryMock.Object, ProductCategoryId = 1},
                 new Product {ProductId = 4, Name = "N4", Description = "D1", Price = 10, ProductCategory = productCategoryMock.Object, ProductCategoryId = 4},
                 new Product {ProductId = 5, Name = "N5", Description = "D1", Price = 10, ProductCategory = productCategoryMock.Object, ProductCategoryId = 1}
            };
            Product productToInsert = new Product { ProductId = 4, Name = "Updated name", Description = "Updated description", Price = 10, ProductCategory = productCategoryMock.Object, ProductCategoryId = 4 };
            mock.Setup(repo => repo.SaveProduct(It.IsAny<Product>())).Returns(productToInsert);
            productCategoryMock.Setup(repo => repo.Name).Returns("Updated category name");
            productCategoryMock.Setup(repo => repo.ProductCategoryId).Returns(4);
            var controller = new ProductApiController(mock.Object);

            // act
            var result = controller.UpdateProduct(productToInsert);

            // assert 
            Assert.Equal("Updated name", result.Value.Name);
            Assert.Equal(4, result.Value.ProductId);
            Assert.Equal("Updated category name", result.Value.ProductCategory.Name);
            Assert.Equal(4, result.Value.ProductCategory.ProductCategoryId);
            Assert.Equal(4, result.Value.ProductCategoryId);
        }

        [Fact]
        public void DeleteProduct_RemovesExistingProductFromDatabase_Ok()
        {
            // arrange
            var mock = new Mock<EFProductRepository>();
            var productCategoryMock = new Mock<ProductCategory>();

            var products = new Product[] {
                 new Product {ProductId = 1, Name = "N1", Description = "D1", Price = 10, ProductCategory = productCategoryMock.Object, ProductCategoryId = 1},
                 new Product {ProductId = 2, Name = "N2", Description = "D1", Price = 10, ProductCategory = productCategoryMock.Object, ProductCategoryId = 1},
                 new Product {ProductId = 3, Name = "N3", Description = "D1", Price = 10, ProductCategory = productCategoryMock.Object, ProductCategoryId = 3},
                 new Product {ProductId = 4, Name = "N4", Description = "D1", Price = 10, ProductCategory = productCategoryMock.Object, ProductCategoryId = 4},
                 new Product {ProductId = 5, Name = "N5", Description = "D1", Price = 10, ProductCategory = productCategoryMock.Object, ProductCategoryId = 1}
            };
            Product productToRemove = new Product { ProductId = 3, Name = "N3", Description = "D1", Price = 10, ProductCategory = productCategoryMock.Object, ProductCategoryId = 3 };
            mock.Setup(repo => repo.DeleteProduct(It.IsAny<int>())).Returns(productToRemove);
            productCategoryMock.Setup(repo => repo.Name).Returns("CG3");
            productCategoryMock.Setup(repo => repo.ProductCategoryId).Returns(3);
            var controller = new ProductApiController(mock.Object);

            // act
            var result = controller.DeleteProduct(3);

            // assert 
            Assert.Equal("N3", result.Value.Name);
            Assert.Equal(3, result.Value.ProductId);
            Assert.Equal("CG3", result.Value.ProductCategory.Name);
            Assert.Equal(3, result.Value.ProductCategory.ProductCategoryId);
            Assert.Equal(3, result.Value.ProductCategoryId);
        }
    }

}