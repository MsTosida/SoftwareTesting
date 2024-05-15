
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System.Collections.Generic;

namespace PasteBinTests
{
    public class PasteBinPage
    {
        private static int WAIT_TIMEOUT_SECONDS = 15;
        private IWebDriver driver;

        [FindsBy(How = How.Id, Using = "postform-text")]
        private IWebElement textAreaInput;

        [FindsBy(How = How.Id, Using = "select2-postform-expiration-container")]
        private IWebElement selectPasteExpiration;

        [FindsBy(How = How.Id, Using = "postform-name")]
        private IWebElement inputPasteNameOrTitle;

        [FindsBy(How = How.XPath, Using = "//button[text()='Create New Paste']")]
        private IWebElement buttonCreateNewPaste;

        [FindsBy(How = How.Id, Using = "select2-postform-format-container")]
        private IWebElement selectSyntaxHighlighting;

        private IWebElement listPasteExpiration;

        public PasteBinPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        public PasteBinPage WaitForPastebinPageLoad()
        {
            driver.Navigate().GoToUrl("https://pastebin.com/");
            new WebDriverWait(driver, TimeSpan.FromSeconds(WAIT_TIMEOUT_SECONDS)).Until(CustomConditions.jQueryAJAXsCompleted());
            return this;
        }

        public PasteBinPage InputMessageInTextarea(string message)
        {
            textAreaInput.SendKeys(message);
            return this;
        }

        public PasteBinPage SelectPasteExpiration(string pasteExpirationText)
        {
            selectPasteExpiration.Click();
            listPasteExpiration = WaitForElementLocatedBy(driver, By.XPath("//li[text()='" + pasteExpirationText + "']"));
            listPasteExpiration.Click();
            return this;
        }

        public PasteBinPage InputPasteName(string pasteName)
        {
            inputPasteNameOrTitle.SendKeys(pasteName);
            return this;
        }

        public PasteBinPage ClickButtonCreateNewPaste()
        {
            buttonCreateNewPaste.Click();
            return this;
        }

        public PasteBinPage SelectSyntaxHighlighting(string syntaxHighlighting)
        {
            selectSyntaxHighlighting.Click();
            IList<IWebElement> liSyntaxHighlightingBash = driver.FindElements(By.XPath("//li[text()='" + syntaxHighlighting + "']"));
            liSyntaxHighlightingBash[0].Click();
            return this;
        }

        public string GetPageTitle()
        {
            Thread.Sleep(1000);
            new WebDriverWait(driver, TimeSpan.FromSeconds(WAIT_TIMEOUT_SECONDS));
            return driver.Title;
        }

        private static IWebElement WaitForElementLocatedBy(IWebDriver driver, By by)
        {
            return new WebDriverWait(driver, TimeSpan.FromSeconds(WAIT_TIMEOUT_SECONDS))
                    .Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(by)).Single();
        }
    }
}
