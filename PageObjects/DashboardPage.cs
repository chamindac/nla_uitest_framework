using NLA.Tests.UI.Utils;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace NLA.Tests.UI.PageObjects
{
    public class DashboardPage : PageObjectBase
    {
        public DashboardPage(DriverFacade driverFacade, IConfiguration configs,
            GlobalSearchElement globalSearch) : base(driverFacade, configs) 
        {
            GlobalSearch = globalSearch;
        }
        #region Element Defintions
        public class GlobalSearchElement : PageObjectBase
        {
            private readonly string _locator = "global-search";
            public GlobalSearchElement(DriverFacade driverFacade, IConfiguration configs) : base(driverFacade, configs) { }

            public bool IsFound 
            {
                get { return DriverFacade.IsWebElementFoundByIdLocator(_locator); }
            }
        }
        #endregion

        public GlobalSearchElement GlobalSearch { get;}
    }
}
