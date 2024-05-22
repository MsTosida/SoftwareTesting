using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace lab_10_01
{ 
    public class A1Page
    {
        private readonly IWebDriver _driver;
        private WebDriverWait _wait;

        public A1Page(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        public void GoToPage()
        {
            _driver.Navigate().GoToUrl("https://www.a1.by/ru/shop/phones/smartphones/Xiaomi/Xiaomi-Redmi-Note-12/grey/p/17.1019616");
        }

        public void CloseCookieBanner()
        {
            IWebElement cookieCloseButton = _wait.Until(ExpectedConditions.ElementExists(By.XPath("//button/span[text() = 'Принять']/ancestor::button")));
            cookieCloseButton.Click();
        }

        public void ClickAvailabilityButton()
        {
            IWebElement availabilityButton = _wait.Until(ExpectedConditions.ElementIsVisible(By.Id("availability-button")));
            availabilityButton.Click();
        }

        public void SelectCity(string cityName)
        {
            IWebElement citiesBox = _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"select-filter-0\"]/div/label/span[1]/span[1]/span")));
            citiesBox.Click();

            IWebElement citiesList = _driver.FindElement(By.XPath("//*[@id=\"select-filter-0\"]/div/label/span[2]/span/span[1]/input"));
            citiesList.SendKeys(cityName);
            citiesList.SendKeys(Keys.Enter);
            Thread.Sleep(2000);
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
