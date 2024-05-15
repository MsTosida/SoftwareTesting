using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System.Collections.ObjectModel;

namespace GoogleCloudTests
{
    public class GoogleCloudHomePage
    {
        private const int WAIT_TIMEOUT_SECONDS = 20;
        private const string HOMEPAGE_URL = "https://cloud.google.com/";
        private IWebDriver driver;
        private string mainWindowHandle;

        [FindsBy(How = How.XPath, Using = "//input[@aria-label='Search']")]
        private IWebElement inputSearch;

        public GoogleCloudHomePage(IWebDriver driver, string mainWindowHandle=null)
        {
            this.driver = driver;
            this.mainWindowHandle = mainWindowHandle;
            PageFactory.InitElements(driver, this);
        }

        public GoogleCloudHomePage OpenPage()
        {
            driver.Navigate().GoToUrl(HOMEPAGE_URL);
            new WebDriverWait(driver, TimeSpan.FromSeconds(WAIT_TIMEOUT_SECONDS));
            return this;
        }

        public GoogleCloudHomePage OpenNewPage()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.open();");
            ReadOnlyCollection<string> handles = driver.WindowHandles;
            foreach (string handle in handles)
            {
                if (handle != mainWindowHandle)
                {
                    driver.SwitchTo().Window(handle);
                    driver.Navigate().GoToUrl(HOMEPAGE_URL);
                }
            }     
            new WebDriverWait(driver, TimeSpan.FromSeconds(WAIT_TIMEOUT_SECONDS));
            return this;
        }

        public GoogleCloudHomePage SearchFromHomePage(string searchText)
        {
            inputSearch.Click();
            inputSearch.SendKeys(searchText + Keys.Enter);
            return this;
        }

    }
}
