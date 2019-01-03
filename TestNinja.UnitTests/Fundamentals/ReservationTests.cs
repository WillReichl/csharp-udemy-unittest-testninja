using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests.Fundamentals
{
    [TestFixture]
    public class ReservationTests
    {
        [Test]
        public void CanBeCancelledBy_UserIsAdmin_ReturnsTrue() // All test methods should be "public void"
        {
            // "Triple A Convention"
            // Arrange: Create objects we want to test
            var reservation = new Reservation();

            // Act
            var result = reservation.CanBeCancelledBy(new User { IsAdmin = true });

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void CanBeCancelledBy_NonAdminMadeReservation_ReturnsTrue()
        {
            var reservation = new Reservation();
            var user = new User { IsAdmin = false };
            reservation.MadeBy = user;
            var result = reservation.CanBeCancelledBy(user);
            Assert.IsTrue(result);
            Assert.That(result, Is.True); // option 2 in NUnit
            Assert.That(result == true); // option 3 in NUnit
        }

        [Test]
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
