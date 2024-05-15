using NUnit.Framework;
using System;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using PasteBinTests;
using OpenQA.Selenium.Edge;


namespace PasteBinTests
{
    [TestFixture]
    public class WebDriverPastebinTest
    {
        private IWebDriver driver;

        [SetUp]
        public void BrowserSetup()
        {
            driver = new EdgeDriver();
            driver.Manage().Window.Maximize();
        }

        [Test]
        public void ICanWinTest()
        {
            string textAreaMessage = "Hello from WebDriver";
            string pasteExpiration = "10 Minutes";
            string pasteName = "helloweb";
            string expectedPageTitle = pasteName + " - Pastebin.com";
            string actualPageTitle = new PasteBinPage(driver)
                    .WaitForPastebinPageLoad()
                    .InputMessageInTextarea(textAreaMessage)
                    .SelectPasteExpiration(pasteExpiration)
                    .InputPasteName(pasteName)
                    .ClickButtonCreateNewPaste()
                    .GetPageTitle();
            Assert.AreEqual(expectedPageTitle, actualPageTitle);
        }

        [Test]
        public void BringItOnTest()
        {
            string pasteName = "how to gain dominance among developers";
            string pasteExpiration = "10 Minutes";
            string syntaxHighlighting = "Bash";
            string expectedPageTitle = pasteName + " - Pastebin.com";
            string textAreaMessage = "git config --global user.name  \"New Sheriff in Town\"\n" +
                                     "git reset $(git commit-tree HEAD^{tree} -m \"Legacy code\")\n" +
                                     "git push origin master --force";

            string actualPageTitle = new PasteBinPage(driver)
                    .WaitForPastebinPageLoad()
                    .InputMessageInTextarea(textAreaMessage)
                    .SelectPasteExpiration(pasteExpiration)
                    .SelectSyntaxHighlighting(syntaxHighlighting)
                    .InputPasteName(pasteName)
                    .ClickButtonCreateNewPaste()
                    .GetPageTitle();
            Assert.AreEqual(expectedPageTitle, actualPageTitle);
        }

        [TearDown]
        public void QuitDriver()
        {
            driver.Quit();
        }
    }
}