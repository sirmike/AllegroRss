using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllegroFinder.Data;

using NUnit.Framework;
using Rhino.Mocks;
using AllegroFinder.Interfaces;
using AllegroFinder.Searcher;

namespace Testing
{
    [TestFixture]
    public class AuctionContainerTest
    {
        [Test]
        public void CreateContainer()
        {
            AuctionsContainer container = new AuctionsContainer();
            Assert.IsEmpty(container.Auctions);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddNullAuction()
        {
            AuctionsContainer container = new AuctionsContainer();
            container.Add((Auction)null);
        }

        [Test]
        public void AddAuction()
        {
            AuctionsContainer container = new AuctionsContainer();
            Auction a = new Auction();
            container.Add(a);

            Assert.IsNotEmpty(container.Auctions);
            Assert.IsTrue(container.Auctions.Count == 1);
        }

        [Test]
        public void AddExistingAuction()
        {
            AuctionsContainer container = new AuctionsContainer();
            Auction a = new Auction();
            a.Id = 123;
            a.Name = "ABC";
            a.Price = 34.0M;
            container.Add(a);

            Auction b = new Auction();
            b.Id = 123;
            b.Name = "DUPA";
            b.Price = 56.0M;
            container.Add(b);

            Assert.IsTrue(container.Auctions.Count == 1);
            Assert.Contains(a, container.Auctions);
            Assert.IsTrue(container.Auctions[0].Name == "DUPA");
            Assert.IsTrue(container.Auctions[0].Price == 56.0M);
        }

        [Test]
        public void ClearContainer()
        {
            AuctionsContainer container = new AuctionsContainer();
            Auction a = new Auction();
            a.Id = 123;
            a.Name = "ABC";
            a.Price = 34.0M;
            container.Add(a);

            container.Clear();

            Assert.IsEmpty(container.Auctions);
        }

        [Test]
        public void ReturnNewAuctions()
        {
            AuctionsContainer container = new AuctionsContainer();
            Auction a = new Auction();
            a.Id = 123;
            a.Name = "ABC";
            a.Price = 34.0M;
            container.Add(a);

            List<Auction> newAuctions = container.GetNewAuctions();
            Assert.IsTrue(newAuctions.Contains(a));

            a.Price = 45.05M;
            container.Add(a);

            newAuctions = container.GetNewAuctions();
            Assert.IsEmpty(newAuctions);

            Auction b = new Auction();
            b.Id = 223;
            b.Name = "ABC";
            b.Price = 34.0M;
            container.Add(b);

            newAuctions = container.GetNewAuctions();
            Assert.Contains(b, newAuctions);
            Assert.IsTrue(newAuctions.Count == 1);
        }
    }
}
