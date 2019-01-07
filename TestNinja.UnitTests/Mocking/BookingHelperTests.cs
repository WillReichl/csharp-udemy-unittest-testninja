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
        private Booking _existingBooking;

        [SetUp]
        public void SetUp()
        {
            _mockBookingRepo = new Mock<IBookingRepository>();
            _existingBooking = new Booking
            {
                Id = 2,
                ArrivalDate = ArriveOn(2017, 1, 15), // 15-JAN-2017 2:00PM
                DepartureDate = DepartOn(2017, 1, 20),
                Reference = "existing"
            };
            _mockBookingRepo.Setup(r => r.GetActiveBookings(1)).Returns(new List<Booking>
            {
                _existingBooking
            }.AsQueryable()); //AsQueryable is important here, cannot implicitly convert List to IQueryable
        }

        [Test]
        public void OverlappingBookingsExist_BookingStartsAndFinishesBeforeAnExistingBooking_ReturnsEmptyString()
        {
            var currentBooking = new Booking
            {
                Id = 1,
                ArrivalDate = Before(_existingBooking.ArrivalDate, daysBefore: 2), // 15-JAN-2017 2:00PM
                DepartureDate = Before(_existingBooking.ArrivalDate, daysBefore: 1),
                Reference = "a"
            };
            var result = BookingHelper.OverlappingBookingsExist(currentBooking, _mockBookingRepo.Object);
            Assert.That(result, Is.EqualTo(""));
        }

        [Test]
        public void OverlappingBookingsExist_BookingStartsBeforeFinishesDuringAnExistingBooking_ReturnsBookingReferenceString()
        {
            var currentBooking = new Booking
            {
                Id = 1,
                ArrivalDate = Before(_existingBooking.ArrivalDate, daysBefore: 2), // 15-JAN-2017 2:00PM
                DepartureDate = After(_existingBooking.ArrivalDate, daysAfter: 1),
                Reference = "current"
            };
            var result = BookingHelper.OverlappingBookingsExist(currentBooking, _mockBookingRepo.Object);
            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }

        [Test]
        public void OverlappingBookingsExist_BookingStartsBeforeFinishesAfterAnExistingBooking_ReturnsBookingReferenceString()
        {
            var currentBooking = new Booking
            {
                Id = 1,
                ArrivalDate = Before(_existingBooking.ArrivalDate, daysBefore: 2), // 15-JAN-2017 2:00PM
                DepartureDate = After(_existingBooking.DepartureDate, daysAfter: 1),
                Reference = "current"
            };
            var result = BookingHelper.OverlappingBookingsExist(currentBooking, _mockBookingRepo.Object);
            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }

        private DateTime Before(DateTime dateTime, int daysBefore)
        {
            return dateTime.AddDays(daysBefore * -1);
        }

        private DateTime After(DateTime dateTime, int daysAfter)
        {
            return dateTime.AddDays(1);
        }

        private DateTime ArriveOn(int year, int month, int day)
        {
            return new DateTime(year, month, day, 14, 0, 0);
        }

        private DateTime DepartOn(int year, int month, int day)
        {
            return new DateTime(year, month, day, 10, 0, 0);
        }
    }
}
