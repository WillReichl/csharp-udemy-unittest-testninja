using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestClass]
    public class ReservationTests
    {
        [TestMethod]
        public void CanBeCancelledBy_UserIsAdmin_ReturnsTrue()
        {
            // "Triple A Convention"
            // Arrange: Create objects we want to test
            var reservation = new Reservation();

            // Act
            var result = reservation.CanBeCancelledBy(new User { IsAdmin = true });

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CanBeCancelledBy_NonAdminMadeReservation_ReturnsTrue()
        {
            var reservation = new Reservation();
            var user = new User { IsAdmin = false };
            reservation.MadeBy = user;
            var result = reservation.CanBeCancelledBy(user);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CanBeCancelledBy_NonAdminNotMadeReservation_ReturnsFalse()
        {
            var reservation = new Reservation();
            var user = new User { IsAdmin = false };
            reservation.MadeBy = user;
            var result = reservation.CanBeCancelledBy(new User { IsAdmin = false }); // different user
            Assert.IsFalse(result);
        }
    }
}
