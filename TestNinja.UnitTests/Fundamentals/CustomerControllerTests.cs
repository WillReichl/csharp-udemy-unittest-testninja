using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests.Fundamentals
{
    [TestFixture]
    class CustomerControllerTests
    {
        private CustomerController _customerController;

        [SetUp]
        public void SetUp ()
        {
            _customerController = new CustomerController();
        }

        [Test]
        public void GetCustomer_IdIsZero_ReturnNotFound ()
        {
            var result = _customerController.GetCustomer(0);

            Assert.That(result, Is.TypeOf<NotFound>()); // NotFound object
            Assert.That(result, Is.InstanceOf<NotFound>()); // NotFound object or derivative of NotFound
            Assert.That(result, Is.InstanceOf<ActionResult>()); // example usage
        }

        [Test]
        public void GetCustomer_IdIsNotZero_ReturnOk ()
        {
            var result = _customerController.GetCustomer(1);

            Assert.That(result, Is.TypeOf<Ok>()); // NotFound object
            Assert.That(result, Is.InstanceOf<Ok>()); // NotFound object or derivative of NotFound
            Assert.That(result, Is.InstanceOf<ActionResult>()); // example usage
        }
    }
}
