using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Lab11_12.Pages
{ 
    public class A1ProductPage
    {
        private WebDriverWait _wait;
        private readonly IWebDriver _driver;


        public A1ProductPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        public A1ProductPage GoToPage()
        {
            _driver.Navigate().GoToUrl("https://www.a1.by/ru/shop/phones/smartphones/Xiaomi/Xiaomi-Redmi-Note-12/grey/p/17.1019616");
            return this;
        }

        public A1ProductPage CloseCookieBanner()
        {
            IWebElement cookieCloseButton = _wait.Until(ExpectedConditions.ElementExists(By.XPath("//button/span[text() = 'Принять']/ancestor::button")));
            cookieCloseButton.Click();
            return this;
        }


        public A1ProductPage ClickAvailabilityButton()
        {
            IWebElement availabilityButton = _wait.Until(ExpectedConditions.ElementIsVisible(By.Id("availability-button")));
            availabilityButton.Click();
            return this;
        }

        public A1ProductPage SelectCity(string cityName)
        {
            IWebElement citiesBox = _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"select-filter-0\"]/div/label/span[1]/span[1]/span")));
            citiesBox.Click();

            IWebElement citiesList = _driver.FindElement(By.XPath("//*[@id=\"select-filter-0\"]/div/label/span[2]/span/span[1]/input"));
            citiesList.SendKeys(cityName);
            citiesList.SendKeys(Keys.Enter);
            Thread.Sleep(2000);
            return this;
        }

        public bool AreAllAddressesContainingCity(string cityName)
        {
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            var addressElements = _wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.CssSelector("p.map-center-info-address-text")));

            bool allContainCity = true;

            foreach (var element in addressElements)
            {
                if (!element.Text.Contains(cityName))
                {
                    allContainCity = false;
                    break;
                }
            }

            return allContainCity;
        }

        public void Close()
        {
            _driver.Quit();
        }
    }
}
