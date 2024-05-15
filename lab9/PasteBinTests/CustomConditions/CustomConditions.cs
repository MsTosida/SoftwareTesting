using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace PasteBinTests
{
    public class CustomConditions
    {
        public static Func<IWebDriver, bool> jQueryAJAXsCompleted()
        {
            return (driver) =>
            {
                return (bool)((IJavaScriptExecutor)driver).ExecuteScript(
                        "return (window.jQuery != null) && (jQuery.active === 0);");
            };
        }
    }
}
