using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using Rhino.Mocks;

using AllegroFinder.Searcher;
using AllegroFinder.Interfaces;
using AllegroFinder.Data;

namespace Testing
{
    [TestFixture]
    public class AuctionSearcherTest
    {
        private MockRepository mocks = null;

        [SetUp]
        public void SetUpMethod()
        {
            mocks = new MockRepository();
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorProviderNull()
        {
            AuctionSearcher searcher = new AuctionSearcher(null, null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorSessionKeyNull()
        {
            AuctionSearcher searcher = new AuctionSearcher(mocks.Stub<IAllegroServiceProvider>(), null);
        }

        [Test]
        public void PrepareSearchQuery()
        {
            SearchOptType query = AllegroServiceProvider.PrepareSearchQuery("Gears of War", 40.0M, null, false);
            Assert.IsTrue(query.searchstring == "Gears of War");
            Assert.IsTrue(query.searchpricefrom == 40.0f);
            Assert.IsTrue(query.searchpriceto == default(float));
            Assert.IsTrue(query.searchoptions == 0);
        }

        [Test]
        public void PrepareSearchQueryOnlyNew()
        {
            SearchOptType query = AllegroServiceProvider.PrepareSearchQuery("Gears of War", null, 45.83M, true);
            Assert.IsTrue(query.searchstring == "Gears of War");
            Assert.IsTrue(query.searchpriceto == 45.83f);
            Assert.IsTrue(query.searchpricefrom == default(float));
            Assert.IsTrue(query.searchoptions == 4);
        }

        [Test]
        public void CallProviderTest()
        {
            IAllegroServiceProvider provider = mocks.StrictMock<IAllegroServiceProvider>();

            using (mocks.Record())
            {
                List<Auction> searchResult = new List<Auction>();
                Auction a1 = new Auction();
                a1.Id = 123;
                a1.Name = "ABC";
                a1.Price = 48.0M;
                searchResult.Add(a1);

                Expect.Call<List<Auction>>(provider.Search("SAMPLE_SESSION", "ABC", null, null, true)).Return(searchResult);
            }

            List<Auction> found = null;
            using (mocks.Playback())
            {
                AuctionSearcher searcher = new AuctionSearcher(provider, "SAMPLE_SESSION");
                searcher.Title = "ABC";
                searcher.OnlyNew = true;

                searcher.DoSearch(out found);
            }

            Assert.IsNotNull(found);
            Assert.That(found[0].Id == 123);
        }
    }
}
