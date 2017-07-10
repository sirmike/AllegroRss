using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using AllegroFinder.Data;

namespace Testing
{
    [TestFixture]
    public class ShortlistTest
    {
        [Test]
        public void AddToList()
        {
            Shortlist list = new Shortlist();
            list.Add("Gears of War");

            Assert.IsTrue(list.Count == 1);
        }

        [Test]
        public void AddToListExisting()
        {
            Shortlist list = new Shortlist();
            list.Add("Gears of War");
            list.Add("GEARS of WAR");

            Assert.IsTrue(list.Count == 1);
        }

        [Test]
        public void DeleteWrongCase()
        {
            Shortlist list = new Shortlist();
            list.Add("Gears of War");
            list.Delete("GEARS OF WAR");

            Assert.IsTrue(list.Count == 0);
        }

        [Test]
        public void Delete()
        {
            Shortlist list = new Shortlist();
            list.Add("Gears of War");
            list.Delete("Gears of War");

            Assert.IsTrue(list.Count == 0);
        }

        [Test]
        public void SetChecked()
        {
            Shortlist list = new Shortlist();
            list.Add("Gears of War");
            list.SetChecked("Gears of WAR");
            
            Assert.IsTrue(list.Games[0].Checked);
        }

        [Test]
        public void SetUnChecked()
        {
            Shortlist list = new Shortlist();
            list.Add("Gears of War");
            list.SetChecked("Gears of WAR");
            list.SetUnchecked("GEARS of WAR");

            Assert.IsFalse(list.Games[0].Checked);
        }

    }
}
