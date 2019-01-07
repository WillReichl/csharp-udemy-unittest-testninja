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
    public class BookingHelperTests
    {
        private Mock<IBookingRepository> _mockBookingRepo;

        [SetUp]
        public void SetUp()
        {
            _mockBookingRepo = new Mock<IBookingRepository>();
        }

        [Test]
        public void OverlappingBookingsExist_BookingStartsAndFinishesBeforeAnExistingBooking_ReturnsEmptyString()
        {
            _mockBookingRepo.Setup(r => r.GetActiveBookings(1)).Returns(new List<Booking>
            {
                new Booking
                {
                    Id = 2,
                    ArrivalDate = new DateTime(2017, 1, 15, 14, 0, 0), // 15-JAN-2017 2:00PM
                    DepartureDate = new DateTime(2017, 1, 20, 10, 0, 0),
                    Reference = "a"
                }
            }.AsQueryable()); //AsQueryable is important here, cannot implicitly convert List to IQueryable

            var currentBooking = new Booking
            {
                Id = 1,
                ArrivalDate = new DateTime(2017, 1, 10, 14, 0, 0), // 15-JAN-2017 2:00PM
                DepartureDate = new DateTime(2017, 1, 14, 10, 0, 0),
                Reference = "a"
            };

            var result = BookingHelper.OverlappingBookingsExist(currentBooking, _mockBookingRepo.Object);

            Assert.That(result, Is.EqualTo(""));

        }
    }
}
