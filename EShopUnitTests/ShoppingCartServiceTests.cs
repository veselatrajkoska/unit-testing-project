using EShop.Domain.DomainModels;
using EShop.Domain.DTO;
using EShop.Domain.Relations;
using EShop.Repository.Interface;
using EShop.Service.Implementation;
using EShop.Service.Interface;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace EShopUnitTests
{
    public class ShoppingCartServiceTests
    {
        private Mock<IShoppingCartService> _mockService;
        private ShoppingCartDto shoppingCartDto;
        private ProductInShoppingCart productInShoppingCart;
        private List<ProductInShoppingCart> list;
        private Guid guid = Guid.NewGuid();

        public ShoppingCartServiceTests()
        {
            _mockService = new Mock<IShoppingCartService>();
            productInShoppingCart = new ProductInShoppingCart
            {
                ProductId = Guid.NewGuid(),
                ShoppingCartId = Guid.NewGuid()
            };
            list = new List<ProductInShoppingCart>
            {
                productInShoppingCart
            };
            shoppingCartDto = new ShoppingCartDto
            {
                Products = list,
                TotalPrice = 250.00
            };
        }

        [Fact]
        public void DeleteProductFromShoppingCartTest()
        {
            _mockService.Setup(x => x.deleteProductFromSoppingCart("1", guid)).Returns(true);
            _mockService.Setup(x => x.deleteProductFromSoppingCart("", guid)).Returns(false);

            var successful = _mockService.Object.deleteProductFromSoppingCart("1", guid);
            var failedEmpty = _mockService.Object.deleteProductFromSoppingCart("", guid);

            Assert.True(successful);
            Assert.False(failedEmpty);
        }

        [Fact]
        public void GetShoppingCartInfoTest()
        {
            _mockService.Setup(x => x.getShoppingCartInfo("1")).Returns(shoppingCartDto);
            _mockService.Setup(x => x.getShoppingCartInfo("")).Returns(new ShoppingCartDto());

            var successful = _mockService.Object.getShoppingCartInfo("1");
            var newCart = _mockService.Object.getShoppingCartInfo("");

            Assert.NotNull(successful);
            Assert.NotNull(newCart);
            Assert.Equal(shoppingCartDto, successful);
            Assert.Empty(newCart.Products);
            Assert.Single(successful.Products);
        }

        [Fact]
        public void OrderTest()
        {
            _mockService.Setup(x => x.order("1")).Returns(true);
            _mockService.Setup(x => x.order("")).Returns(false);

            var successful = _mockService.Object.order("1");
            var failed = _mockService.Object.order("");

            Assert.True(successful);
            Assert.False(failed);
        }
    }
}
