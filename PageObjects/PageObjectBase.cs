using NLA.Tests.UI.Utils;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace NLA.Tests.UI.PageObjects
{
    public class PageObjectBase
    {
        public PageObjectBase(DriverFacade driverFacade, IConfiguration configs)
        {
            DriverFacade = driverFacade;
            Configs = configs;
        }

        public DriverFacade DriverFacade { get; }
        public IConfiguration Configs { get; }
    }
}
