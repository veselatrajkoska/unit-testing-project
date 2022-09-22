using EShop.Domain;
using EShop.Domain.DomainModels;
using EShop.Domain.Identity;
using EShop.Repository.Interface;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace EShopUnitTests
{
    public class OrderServiceTests
    {
        private readonly Mock<IOrderRepository> _mockRepository = new Mock<IOrderRepository>();
        private Order mockOrder;
        private List<Order> mockOrdersList;
        private BaseEntity mockModel;

        public OrderServiceTests()
        {
            mockOrder = new Order
            {
                UserId = "1",
                User = new EShopApplicationUser()
            };
            mockOrdersList = new List<Order>();
            mockOrdersList.Add(mockOrder);
            mockModel = new BaseEntity();
        }

        [Fact]
        public void GetAllOrdersTest()
        {
            _mockRepository.Setup(x => x.getAllOrders()).Returns(mockOrdersList);

            var orders = _mockRepository.Object.getAllOrders();

            Assert.NotNull(orders);
            Assert.Single(orders);
        }

        [Fact]
        public void GetOrderDetailsTest()
        {
            _mockRepository.Setup(x => x.getOrderDetails(mockModel)).Returns(mockOrder);

            var orderDetails = _mockRepository.Object.getOrderDetails(mockModel);

            Assert.NotNull(orderDetails);
            Assert.Equal(mockOrder.UserId, orderDetails.UserId);
        }
    }
}
