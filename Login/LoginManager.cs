using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AllegroFinder.Interfaces;
using AllegroFinder.Base;
using System.Threading;
using System.Web.Services.Protocols;

namespace AllegroFinder.Data
{
    public class LoginManager : ConnectorBase
    {
        private const int defaultCountryCode = 1; // PL
        
        private string sessionKey = null;
        private string apiKey;
        private string user;
        private string password;
        private int countryCode = 0;

        private int unsuccessfulLoginCounter = 0;

        private void ValidateCredentials()
        {
            if (string.IsNullOrEmpty(user))
            {
                throw new ArgumentNullException("User", "cannot be null or empty");
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException("Password", "cannot be null or empty");
            }

            if (string.IsNullOrEmpty(apiKey))
            {
                throw new ArgumentNullException("ApiKey", "cannot be null or empty");
            }
        }

        public string SessionKey
        {
            get
            {
                return sessionKey;
            }
        }

        public int UnsuccessfulLoginCounter
        {
            get
            {
                return unsuccessfulLoginCounter;
            }
        }

        public LoginManager(IAllegroServiceProvider provider, string apiKey, string user, string password) : base(provider)
        {
            this.countryCode = defaultCountryCode;
            this.apiKey = apiKey;
            this.user = user;
            this.password = password;

            ValidateCredentials();
        }

        public MethodResult Login()
        {
            ValidateCredentials();
            MethodResult result = new MethodResult();
            try
            {
                sessionKey = serviceProvider.Login(user, password, countryCode, apiKey);
                unsuccessfulLoginCounter = 0;
            }
            catch (SoapException ex)
            {
                unsuccessfulLoginCounter++;
                result.ErrorCode = ex.Code.Name;
                result.ErrorMessage = ex.Message;
                return result;
            }
            catch (Exception ex)
            {
                unsuccessfulLoginCounter++;
                result.ErrorMessage = ex.Message;
                result.ErrorCode = "CANNOT_CONNECT";
                return result;
            }
            if (string.IsNullOrEmpty(sessionKey))
            {
                result.ErrorCode = "UNKNOWN_ERROR";
                result.ErrorMessage = "No error from Allegro but session is empty";
            }
            return result;
        }
    }
}
