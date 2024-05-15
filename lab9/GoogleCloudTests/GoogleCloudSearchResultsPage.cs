using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;

namespace GoogleCloudTests
{
    public class GoogleCloudSearchResultPage
    {
        private const int WAIT_TIMEOUT_SECONDS = 20;
        private IWebDriver driver;

        [FindsBy(How = How.XPath, Using = "//b[text()='Google Cloud Pricing Calculator']")]
        private IWebElement linkToTheCalculatorPage;

        public GoogleCloudSearchResultPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        public GoogleCloudSearchResultPage SelectSearchResult(string searchText)
        {
            WebDriverWait waitForPricingCalculatorPage = new WebDriverWait(driver, TimeSpan.FromSeconds(WAIT_TIMEOUT_SECONDS));
            waitForPricingCalculatorPage.Until(ExpectedConditions.ElementIsVisible(By.XPath("//b[text()='" + searchText + "']")));
            linkToTheCalculatorPage.Click();
            return this;
        }
    }
}
