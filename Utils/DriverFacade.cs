using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NLA.Tests.UI.Utils
{
    public class DriverFacade
    {
        private readonly IWebDriver _webDriver;
        private readonly WebDriverWait _defaultWait;
        
        //public DriverFacade(IConfiguration configs)
        public DriverFacade(IConfiguration configs)
        {
            // We are using Chrome, but you can use any webDriver
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            options.AddArgument("--disable-notifications");

            _webDriver = new ChromeDriver(options);
            _defaultWait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(int.Parse(configs["System:DefaultWwaitSeconds"])));
            _defaultWait.IgnoreExceptionTypes(typeof(NoSuchElementException),typeof(StaleElementReferenceException));
        }

        #region Navigate
        public void Navigate(string url)
        {
            _webDriver.Url = url;
        }
        #endregion

        #region Get Element Text
        public string GetWebElementTextByIdLocator(string elementId)
        {
            var webElement = _defaultWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                .ElementIsVisible(By.Id(elementId)));
            return webElement.Text;
        }

        public string GetWebElementTextByCssLocator(string elementCss)
        {
            var webElement = _defaultWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                .ElementIsVisible(By.CssSelector(elementCss)));
            return webElement.Text;
        }
        #endregion

        #region Set Element Text
        public void SetWebElementTextByIdLocator(string elementId, string elementText)
        {
           var webElement = _defaultWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions
               .ElementToBeClickable(By.Id(elementId)));
            webElement.SendKeys(elementText);
        }

        public void SetWebElementTextByCssLocator(string elementCss, string elementText)
        {
            var webElement = _defaultWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                .ElementToBeClickable(By.CssSelector(elementCss)));
            webElement.SendKeys(elementText);
        }
        #endregion

        #region Clear Element Text
        public void ClearWebElementTextByIdLocator(string elementId)
        {
            var webElement = _defaultWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                .ElementToBeClickable(By.Id(elementId)));
            webElement.Clear();
        }

        public void ClearWebElementTextByCssLocator(string elementCss)
        {
            var webElement = _defaultWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                .ElementToBeClickable(By.CssSelector(elementCss)));
            webElement.Clear();
        }
        #endregion

        #region Set Element Value Attribute
        public void SetWebElementValueAttributeByIdLocator(string elementId, string elementText)
        {
            var webElement = _defaultWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                .ElementToBeClickable(By.Id(elementId)));
            webElement.SetAttribute("value", elementText);
        }

        public void SetWebElementValueAttributeByCssLocator(string elementCss, string elementText)
        {
            var webElement = _defaultWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                .ElementToBeClickable(By.CssSelector(elementCss)));
            webElement.SetAttribute("value", elementText);
        }
        #endregion

        #region Wait For Element Text
        public void WaitForWebElementTextByIdLocator(string elementId, string elementText)
        {
            _defaultWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TextToBePresentInElementLocated(
                By.Id(elementId), elementText));
        }

        public void WaitForWebElementTextByCssLocator(string elementCss, string elementText)
        {
            _defaultWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TextToBePresentInElementLocated(
                By.CssSelector(elementCss), elementText));
        }
        #endregion

        #region Wait For Element To Be Invisible
        public void WaitForWebElementToBeInvisibletByIdLocator(string elementId)
        {
            _defaultWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                .InvisibilityOfElementLocated(By.Id(elementId)));
        }

        public void WaitForWebElementToBeInvisibletByCssLocator(string elementCss)
        {
            _defaultWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                .InvisibilityOfElementLocated(By.CssSelector(elementCss)));
        }
        #endregion

        #region Click Element
        public void ClickWebElementByIdLocator(string elementId)
        {
            var webElement = _defaultWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                .ElementToBeClickable(By.Id(elementId)));
            webElement.Click();
        }

        public void ClickWebElementByCssLocator(string elementCss)
        {
            var webElement = _defaultWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                .ElementToBeClickable(By.CssSelector(elementCss)));
            webElement.Click();
        }

        public void ClickWebElementByXPathLocator(string elementXPath)
        {
            var webElement = _defaultWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                .ElementToBeClickable(By.XPath(elementXPath)));
            webElement.Click();
        }
        #endregion

        #region Is Element Found
        public bool IsWebElementFoundByIdLocator(string elementId)
        {
            IWebElement element;
            try
            {
                element = _defaultWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                    .ElementExists(By.Id(elementId)));
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }

            return element != null;
        }

        public bool IsWebElementFoundByCssLocator(string elementCss)
        {
            IWebElement element;
            try
            {
                element = _defaultWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                    .ElementExists(By.CssSelector(elementCss)));
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }

            return element != null;
        }
        #endregion

        public void Quit()
        {
            _webDriver.Quit();
        }
    }
}
