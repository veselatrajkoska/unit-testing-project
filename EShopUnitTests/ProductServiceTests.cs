using EShop.Domain.DomainModels;
using EShop.Domain.DTO;
using EShop.Domain.Relations;
using EShop.Repository.Interface;
using EShop.Service.Interface;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace EShopUnitTests
{
    public class ProductServiceTests
    {
        private Mock<IProductService> _mockService = new Mock<IProductService>();
        private Mock<IRepository<Product>> _mockProductRepository = new Mock<IRepository<Product>>();
        private Product product = new Product
        {
            ProductName = "Product",
            ProductImage = "",
            ProductDescription = "Very nice",
            ProductPrice = 150.0,
            ProductRating = 4.5,
        };
        private AddToShoppingCardDto addToShoppingCardDto = new AddToShoppingCardDto
        {
            SelectedProduct = new Product(),
            SelectedProductId = Guid.NewGuid(),
            Quantity = 2
        };
        List<Product> productsList = new List<Product>();

        public ProductServiceTests()
        {
            _mockService = new Mock<IProductService>();
        }

        [Fact]
        public void AddToShoppingCartTest()
        {
            _mockService.Setup(x => x.AddToShoppingCart(addToShoppingCardDto, "1")).Returns(true);
            _mockService.Setup(x => x.AddToShoppingCart(addToShoppingCardDto, null)).Returns(false);

            var addProductTrueResult = _mockService.Object.AddToShoppingCart(addToShoppingCardDto, "1");
            var addProductFalseResult = _mockService.Object.AddToShoppingCart(addToShoppingCardDto, null);

            Assert.True(addProductTrueResult);
            Assert.False(addProductFalseResult);
        }

        [Fact]
        public void GetAllProductsTest()
        {
            productsList.Add(product);
            _mockService.Setup(x => x.GetAllProducts()).Returns(productsList);

            var products = _mockService.Object.GetAllProducts();

            Assert.NotNull(products);
            Assert.Single(products);
        }

        [Fact]
        public void GetDetailsForProductTest()
        {
            _mockService.Setup(x => x.GetDetailsForProduct(product.Id)).Returns(product);

            var details = _mockService.Object.GetDetailsForProduct(product.Id);

            Assert.NotNull(details);
            Assert.Equal(product, details);
        }

        [Fact]
        public void GetShoppingCartInfoTest()
        {
            _mockService.Setup(x => x.GetShoppingCartInfo(product.Id)).Returns(addToShoppingCardDto);

            var cartInfo = _mockService.Object.GetShoppingCartInfo(product.Id);

            Assert.NotNull(cartInfo);
            Assert.Equal(addToShoppingCardDto, cartInfo);
        }
    }
}
