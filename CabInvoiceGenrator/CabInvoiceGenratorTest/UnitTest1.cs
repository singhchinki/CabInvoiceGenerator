using CabInvoiceGenerator;
using NUnit.Framework;

namespace CabInvoiceGenratorTest
{
    public class Tests
    {
        InvoiceGenerator invoiceGenerator = null;
        RideRepository rideRepository;
        /// <summary>
        /// Test case 1 Given the distance and time when invoice generator then should return total fare.
        /// </summary>
        [Test]
        public void GivenDistanceAndTime_WhenInvoiceGenerator_ThenShouldReturnTotalFare()
        {
            invoiceGenerator = new InvoiceGenerator(RideType.NORMAL);
            double distance = 2.0;
            int time = 5;
            double fare = invoiceGenerator.CalculateFare(distance, time);
            double expected = 25;
            Assert.AreEqual(expected, fare);
        }
        [Test]
        public void GivenMultipleRides_WhenInvoiceGenerator_thenShouldReturnInvoiceSummary()
        {
            invoiceGenerator = new InvoiceGenerator(RideType.NORMAL);
            Ride[] rides = { new Ride(2.0, 5), new Ride(0.1, 1) };
            InvoiceSummary invoiceSummary = invoiceGenerator.CalculateFare(rides);
            InvoiceSummary expectedSummary = new InvoiceSummary(2, 30.0);
            Assert.AreEqual(expectedSummary, invoiceSummary);
        }
        [Test]
        public void GivenMultipleRides_WhenInvoiceGenerator_thenShouldReturnFollowingInvoiceSummary()
        {
            invoiceGenerator = new InvoiceGenerator(RideType.NORMAL);
            Ride[] rides = { new Ride(2.0, 5), new Ride(0.1, 1) };
            InvoiceSummary invoiceSummary = invoiceGenerator.CalculateAvrageFare(rides);
            InvoiceSummary expectedSummary = new InvoiceSummary(2, 30.0, 15);
            Assert.AreEqual(expectedSummary, invoiceSummary);
        }
        [Test]
        public void GivenMultipleRides_WhenUserId_thenShouldReturnFollowingInvoiceSummary()
        {
            invoiceGenerator = new InvoiceGenerator(RideType.NORMAL);
            Ride[] rides = { new Ride(2.0, 5), new Ride(0.1, 1) };
            RideRepository rideRepository = new RideRepository();
            string userId = "Dhiraj";
            rideRepository.AddRides(userId, rides);
            Ride[] rideData = rideRepository.GetRides(userId);
            InvoiceSummary invoiceSummary = invoiceGenerator.CalculateAvrageFare(rideData);
            InvoiceSummary expectedSummary = new InvoiceSummary(2, 30.0, 15);
            Assert.AreEqual(expectedSummary, invoiceSummary);
        }
    }

}