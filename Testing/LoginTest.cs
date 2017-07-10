using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using Rhino.Mocks;

using AllegroFinder.Data;
using AllegroFinder.Interfaces;

namespace Testing
{
    [TestFixture]
    public class LoginTest
    {
        private const string ApiKey = "key";
        private MockRepository mocks = null;

        [SetUp]
        public void SetUpMethod()
        {
            mocks = new MockRepository();
        }

        [Test]
        public void Login()
        {
            IAllegroServiceProvider provider = mocks.Stub<IAllegroServiceProvider>();
            LoginManager lm = new LoginManager(provider, ApiKey, "login", "haslo");

            using (mocks.Record())
            {
                SetupResult.For(provider.Login("login", "haslo", 1, ApiKey)).IgnoreArguments().Return("sample_session_key");
            }

            using (mocks.Playback())
            {
                MethodResult result = lm.Login();
                Assert.IsTrue(result.Success);
            }
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void LoginEmptyCredentials()
        {
            IAllegroServiceProvider provider = mocks.Stub<IAllegroServiceProvider>();
            LoginManager lm = new LoginManager(provider, ApiKey, null, string.Empty);
            lm.Login();
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void LoginEmptyApiKey()
        {
            IAllegroServiceProvider provider = mocks.Stub<IAllegroServiceProvider>();
            LoginManager lm = new LoginManager(provider, string.Empty, string.Empty, string.Empty);
        }

        [Test]
        public void LoginUnsuccessful()
        {
            IAllegroServiceProvider provider = mocks.Stub<IAllegroServiceProvider>();
            LoginManager lm = new LoginManager(provider, "user", "password", ApiKey);

            using (mocks.Record())
            {
                SetupResult.For(provider.Login("user", "password", 1, ApiKey)).IgnoreArguments().Return(null);
            }

            using (mocks.Playback())
            {
                MethodResult result = lm.Login();
                Assert.IsFalse(result.Success);
            }
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void LoginNullProvider()
        {
            LoginManager lm = new LoginManager(null, "user", "password", ApiKey);
        }
    }
}
