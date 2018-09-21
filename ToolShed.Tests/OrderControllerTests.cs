using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using ToolShed.Web.Controllers;
using ToolShed.Web.Models;
using ToolShed.Web.Repositories;
using Xunit;

namespace ToolShed.Tests
{
    public class OrderControllerTests
    {
        [Fact]

        public void Not_Able_To_Checkout_With_Empty_Cart()
        {
            //Arrange
            var mock = new Mock<IOrderRepository>();
            var cart = new Cart();
            var order = new Order();
            var controller = new OrderController(mock.Object, cart);

            //Act controller.Checkout  ?
            ViewResult result = controller.Checkout(order) as ViewResult;

            //Assert
            mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Never);
            Assert.True(string.IsNullOrEmpty(result.ViewName));
            Assert.False(result.ViewData.ModelState.IsValid);

        }



    }
}
