using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class OrderServiceTests
    {
        [Test]
        public void PlaceOrder_WhenCalled_StoreTheOrder()
        {
            var storage = new Mock<IStorage>(); // Arrange
            var service = new OrderService(storage.Object); // Arrange

            var order = new Order(); // created on its own line here, so same object can be passed to both method we're testing and Moq's Verify expression
            service.PlaceOrder(order); // Act

            // Verify will test that the method in the expression was called, and was called using the same parameter we include in this expression.
            // We are testing only that "Store" was called and passed an order. We are NOT testing the *implementation*. That would be a full integration test.
            storage.Verify(s => s.Store(order)); // Assert

        }
    }
}
