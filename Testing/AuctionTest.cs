using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using AllegroFinder.Data;
using AllegroFinder.Exceptions;

namespace Testing
{
    [TestFixture]
    public class AuctionTest
    {
        [Test]
        public void EquatableTest()
        {
            Auction a = new Auction();
            a.Id = 10;
            a.Name = "FOO";

            Auction b = new Auction();
            b.Id = 10;
            b.Name = "BAR";

            Assert.IsTrue(a.Equals(b));
        }

        [Test]
        [ExpectedException(typeof(AuctionValidationException))]
        public void ValidationTest()
        {
            Auction a = new Auction();
            a.Validate();
        }

        [Test]
        public void TimeLeftAsStringTestFull()
        {
            Auction a = new Auction();
            a.TimeLeft = TimeSpan.Parse("13.2:32:12");

            Assert.IsTrue(a.TimeLeftAsString == "13 dni");
        }

        [Test]
        public void TimeLeftAsStringTestOnlyDays()
        {
            Auction a = new Auction();
            a.TimeLeft = TimeSpan.Parse("13");

            Assert.IsTrue(a.TimeLeftAsString == "13 dni");
        }

        [Test]
        public void TimeLeftAsStringTestOnlyHours()
        {
            Auction a = new Auction();
            a.TimeLeft = TimeSpan.Parse("0.23:0:0");

            Assert.IsTrue(a.TimeLeftAsString == "23 godz.");
        }

        
        [Test]
        public void TimeLeftAsStringTestDaysNoHoursMinutes()
        {
            Auction a = new Auction();
            a.TimeLeft = TimeSpan.Parse("2.0:12:0");

            Assert.IsTrue(a.TimeLeftAsString == "2 dni");
        }

        [Test]
        public void TimeLeftAsStringTestNoDaysNoHoursMinutes()
        {
            Auction a = new Auction();
            a.TimeLeft = TimeSpan.Parse("0.0:12:0");

            Assert.IsTrue(a.TimeLeftAsString == "12 min.");
        }

        [Test]
        public void TimeLeftAsStringLessThanMinute()
        {
            Auction a = new Auction();
            a.TimeLeft = TimeSpan.Parse("0.0:0:34");

            Assert.IsTrue(a.TimeLeftAsString == "Mniej niż minuta");
        }

        [Test]
        public void TimeLeftAsStringOneDay()
        {
            Auction a = new Auction();
            a.TimeLeft = TimeSpan.Parse("1.0:34:0");

            Assert.IsTrue(a.TimeLeftAsString == "1 dzień");
        }

        [Test]
        public void IsAuctionNew()
        {
            Auction a = new Auction();
            a.Id = 10;
            a.Name = "FOO";
            a.IsNew = true;

            Assert.IsTrue(a.IsNew);
        }
    }
}
