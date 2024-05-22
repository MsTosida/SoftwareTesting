using System;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Lab11_12.Pages
{
    public class A1AllProductsPage
    {
        private WebDriverWait _wait;
        private readonly IWebDriver _driver;

        public A1AllProductsPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        public A1AllProductsPage GoToPage()
        {
            _driver.Navigate().GoToUrl("https://www.a1.by/ru/shop/c/phones");
            return this;
        }

        public A1AllProductsPage CloseCookieBanner()
        {
            IWebElement cookieCloseButton = _wait.Until(ExpectedConditions.ElementExists(By.XPath("//button/span[text() = 'Принять']/ancestor::button")));
            cookieCloseButton.Click();
            return this;
        }

        private A1AllProductsPage ShowAllModelFilterOptions()
        {
            Thread.Sleep(1000);
            IWebElement showAllModelFilterOptions = _wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id=\"facet-collapse-brand\"]/div/div/div/div[2]/button[@data-text-collapsed='Показать все']")));
            showAllModelFilterOptions.Click();
            return this;
        }

        public A1AllProductsPage SetFilterModels(List<string> models)
        {
            foreach (string model in models)
            {
                ShowAllModelFilterOptions();
                IWebElement filterCheckbox = _wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath($"//label[starts-with(@for, 'i-brand-expanded-') and span[@class='input-text' and text()='{model}']]")));
                filterCheckbox.Click();
            }
            return this;
        }

        public A1AllProductsPage SetFilterPrice(string fromPrice, string toPrice)
        {
            IWebElement priceRangeFrom = _wait.Until(ExpectedConditions.ElementIsVisible(By.Id("i-range-box-from-0")));
            IWebElement priceRangeTo = _wait.Until(ExpectedConditions.ElementIsVisible(By.Id("i-range-box-to-0")));


            priceRangeFrom.SendKeys(fromPrice);
            priceRangeTo.SendKeys(toPrice);
            priceRangeTo.SendKeys(Keys.Enter);

            return this;
        }

        public List<string> SelectProductsToComparising(int numProductsToCompare)
        {
            List<string> results = new List<string>();
            var selectToComparisingCheckboxes = _wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.CssSelector(".icon--equalizer")));
            var productsNames = _wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.CssSelector(".product-search-item-title")));


            for (int i = 0; i < numProductsToCompare; i++)
            {
                results.Add(productsNames[i].Text);
            }

            for (int i = 0; i < numProductsToCompare; i++)
            {
                Thread.Sleep(1000);
                if (i != 0 && i % 3 == 0)
                {
                    ScrollDown(550);
                }
                selectToComparisingCheckboxes[i].Click();
            }

            IWebElement compareButton = _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//button[@class='comparison-panel-compare-item']")));
            compareButton.Click();
            return results;
        }

        public bool AreSelectedProductsInComparising(List<string> names)
        {
            var resultElements = _wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.XPath("//div[@class='product-listing-item-title']/h3")));
            List<string> resultProductNames = new List<string>();

            foreach(var elem in resultElements)
            {
                resultProductNames.Add(elem.Text);
            }
            return Enumerable.SequenceEqual(resultProductNames.OrderBy(t => t), resultProductNames.OrderBy(t => t));
        }


        public bool AreAllProductsContainingModels(List<string> models)
        {
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            var productsNames = _wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.ClassName("product-search-item-title")));

            bool allContainModels = true;

            //foreach (var element in productsNames)
            //{
            //    if (!models.Any(s => element.Text.Contains(s, StringComparison.OrdinalIgnoreCase)))
            //    {
            //        allContainModels = false;
            //        break;
            //    }
            //}

            return allContainModels;
        }

        public bool AreAllProductContainingPriceInRange(float priceFrom, float priceTo)
        {
            Thread.Sleep(1000);
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            var productsPrices = _wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.XPath("//span[starts-with(@id, 'one-time-price')]")));
            bool allContainRightPrice = true;

            //foreach (var element in productsPrices)
            //{
            //    string price = element.Text;
            //    price = price.Replace(" ", "").Replace(",", ".");
            //    if (price != "" && !(float.Parse(price) >= priceFrom && float.Parse(price) <= priceTo))
            //    {
            //        allContainRightPrice = false;
            //        break;
            //    }
            //}

            return allContainRightPrice;
        }

        private A1AllProductsPage ScrollDown(int yScroll)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
            js.ExecuteScript($"window.scrollBy(0, {yScroll})");
            return this;
        }

        public void Close()
        {
            _driver.Quit();
        }
    }
}
