using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;

namespace Lab11_12.Pages
{
    public class A1HomePage
    {
        private WebDriverWait _wait;
        private readonly IWebDriver _driver;

        [FindsBy(How = How.Id, Using = "dropdownGlobalSearch")]
        private IWebElement buttonOpenSearch;

        public A1HomePage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            PageFactory.InitElements(_driver, this);
        }

        public A1HomePage GoToPage()
        {
            _driver.Navigate().GoToUrl("https://www.a1.by/ru/");
            return this;
        }

        public A1HomePage CloseCookieBanner()
        {
            IWebElement cookieCloseButton = _wait.Until(ExpectedConditions.ElementExists(By.XPath("//button/span[text() = 'Принять']/ancestor::button")));
            cookieCloseButton.Click();
            return this;
        }

        public A1HomePage SearchTextForProducts(string text)
        {
            buttonOpenSearch.Click();

            IWebElement searchInput = _wait.Until(ExpectedConditions.ElementIsVisible(By.Id("i-global-search-input")));
            searchInput.SendKeys(text);
            searchInput.Submit();
            return this;
        }

        public bool AreProductsContainSearchText(string text)
        {
            var productsNames = _wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.ClassName("product-search-item-title")));

            bool allContainText = true;

            foreach (var productName in productsNames)
            {
                string searchText = text.Replace(" ", "").ToUpper();
                string resultProductName = productName.Text.Replace(" ", "").ToUpper();
                if (!resultProductName.Contains(searchText))
                    allContainText = false;
            }

            return allContainText;
        }

    }
}
