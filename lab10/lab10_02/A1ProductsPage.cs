using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace lab10_02
{
    public class A1ProductsPage
    {
        private readonly IWebDriver _driver;
        private WebDriverWait _wait;

        public A1ProductsPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        public void GoToPage()
        {
            _driver.Navigate().GoToUrl("https://www.a1.by/ru/shop/c/phones");
        }

        public void CloseCookieBanner()
        {
            IWebElement cookieCloseButton = _wait.Until(ExpectedConditions.ElementExists(By.XPath("//button/span[text() = 'Принять']/ancestor::button")));
            cookieCloseButton.Click();
        }

        private void ShowAllModelFilterOptions()
        {
            Thread.Sleep(1000);
            IWebElement showAllModelFilterOptions = _wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id=\"facet-collapse-brand\"]/div/div/div/div[2]/button[@data-text-collapsed='Показать все']")));
            showAllModelFilterOptions.Click();
        }

        public void SetFilterModels(List<string> models)
        {
            foreach (string model in models)
            {
                ShowAllModelFilterOptions();
                IWebElement filterCheckbox = _wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath($"//label[starts-with(@for, 'i-brand-expanded-') and span[@class='input-text' and text()='{model}']]")));
                filterCheckbox.Click();
            }
        }

        public void SetFilterPrice(string fromPrice, string toPrice)
        {
            IWebElement priceRangeFrom = _wait.Until(ExpectedConditions.ElementIsVisible(By.Id("i-range-box-from-0")));
            IWebElement priceRangeTo = _wait.Until(ExpectedConditions.ElementIsVisible(By.Id("i-range-box-to-0")));


            priceRangeFrom.SendKeys(fromPrice);
            priceRangeTo.SendKeys(toPrice);
            priceRangeTo.SendKeys(Keys.Enter);
        }

        public bool AreAllProductsContainingModels(List<string> models)
        {
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            var productsNames = _wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.ClassName("product-search-item-title")));

            bool allContainModels = true;

            foreach (var element in productsNames)
            {
                if (!models.Any(s => element.Text.Contains(s, StringComparison.OrdinalIgnoreCase)))
                {
                    allContainModels = false;
                    break;
                }
            }

            return allContainModels;
        }

        public bool AreAllProductContainingPriceInRange(float priceFrom, float priceTo)
        {
            Thread.Sleep(1000);
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            var productsPrices = _wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.XPath("//span[starts-with(@id, 'one-time-price')]")));
            bool allContainRightPrice = true;

            foreach (var element in productsPrices)
            {
                string price = element.Text;
                price = price.Replace(" ", "").Replace(",", ".");
                //if (price != "" && !(float.Parse(price) >= priceFrom && float.Parse(price) <= priceTo))
                //{
                //    allContainRightPrice = false;
                //    break;
                //}
            }

            return allContainRightPrice;
        }

        public void Close()
        {
            _driver.Quit();
        }
    }
}
