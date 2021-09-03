using OpenQA.Selenium;
using OpenQA.Selenium.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace NLA.Tests.UI.Utils
{
    static class SeleniumExtensions
    {
        public static IWebElement SetAttribute(this IWebElement element, string name, string value)
        {
            var driver = ((IWrapsDriver)element).WrappedDriver;
            var jsExecutor = (IJavaScriptExecutor)driver;
            jsExecutor.ExecuteScript("arguments[0].setAttribute(arguments[1], arguments[2]);", element, name, value);

            return element;
        }
    }
}
