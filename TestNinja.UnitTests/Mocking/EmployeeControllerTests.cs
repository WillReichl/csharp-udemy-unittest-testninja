using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class EmployeeControllerTests
    {
        private Mock<IEmployeeStorage> _employeeStorage;
        private EmployeeController _controller;

        [SetUp]
        public void SetUp()
        {
            _employeeStorage = new Mock<IEmployeeStorage>();
            _controller = new EmployeeController(_employeeStorage.Object);
        }

        [Test]
        public void DeleteEmployee_WhenCalled_DeleteTheEmployeeFromDb()
        {
            var result = _controller.DeleteEmployee(1);
            _employeeStorage.Verify(s => s.DeleteEmployee(1)); // ensures that storage layer method called w/ expected parameter
            Assert.That(result, Is.TypeOf<RedirectResult>()); // ensures that expected return type comes back
        }

        // Note 1: We do not test the actual delete from the database in EmployeeStorage. That is an implementation detail,
        //         and we do not unit test those. That will be an integration test.

        // Note 2: To ensure viability of this test, you can change the EmployeeController.DeleteEmployee method to 
        //         pass (id+1). The test will correctly fail, since, though storage layer method was called, and an 
        //         int was passed, it's not the expected one.
    }
}
