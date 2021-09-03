using NLA.Tests.UI.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace NLA.Tests.UI.PageObjects
{
    public class LoginPage : PageObjectBase
    {
        public LoginPage(DriverFacade driverFacade, IConfiguration configs,
            UserNameElement userName,
            PasswordElement password,
            AccountElement account,
            SignInElement signIn):base(driverFacade, configs) 
        {
            UserName = userName;
            Password = password;
            Account = account;
            SignIn = signIn;
        }

        #region Element Defintions
        public class UserNameElement : PageObjectBase
        {
            private readonly string _locator = "UserName";
            public UserNameElement(DriverFacade driverFacade, IConfiguration configs) : base(driverFacade, configs) { }

            public string Text {
                set
                {
                    DriverFacade.ClearWebElementTextByIdLocator(_locator);
                    DriverFacade.SetWebElementTextByIdLocator(_locator, value);
                } 
            }
        }

        public class PasswordElement : PageObjectBase
        {
            private readonly string _locator = "Password";
            public PasswordElement(DriverFacade driverFacade, IConfiguration configs) : base(driverFacade, configs) { }

            public string Text
            {
                set
                {
                    DriverFacade.ClearWebElementTextByIdLocator(_locator);
                    DriverFacade.SetWebElementTextByIdLocator(_locator, value);
                }
            }
        }

        public class AccountElement : PageObjectBase
        {
            private readonly string _locator = "TenantName";
            public AccountElement(DriverFacade driverFacade, IConfiguration configs) : base(driverFacade, configs) { }

            public string Text
            {
                set
                {
                    DriverFacade.ClearWebElementTextByIdLocator(_locator);
                    DriverFacade.SetWebElementTextByIdLocator(_locator, value);
                }
            }
        }

        public class SignInElement : PageObjectBase
        {
            private readonly string _locator = "button.btn.green.btn-circle";
            public SignInElement(DriverFacade driverFacade, IConfiguration configs) : base(driverFacade, configs) { }

            public void Click()
            {
                DriverFacade.ClickWebElementByCssLocator(_locator);
            }
        }
        #endregion

        public UserNameElement UserName { get; }
        public PasswordElement Password { get; }
        public AccountElement Account { get; }
        public SignInElement SignIn { get; }

        public void Navigate()
        {
            DriverFacade.Navigate(Configs["PageUrls:LoginPage"]);
        }

    }
}
