using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;
using OpenQA.Selenium.Edge;

namespace GoogleCloudTests
{
    public class Tests
    {
        [TestFixture]
        public class GoogleCloudTest
        {
            private IWebDriver _driver;

            [SetUp]
            public void BrowserSetup()
            {
                _driver = new EdgeDriver();
                _driver.Manage().Window.Maximize();
            }

            // Test was created for calculator legacy version
            [Test]
            public void HurtMePlentyTest()
            {
                string expectedTotalCost = "Total Estimated Cost: USD 1,081.20 per 1 month";
                string searchText = "Google Cloud Platform Pricing Calculator";
                string whatAreInstancesFor = "";
                int numberOfInstances = 4;

                new GoogleCloudHomePage(_driver)
                    .OpenPage()
                    .SearchFromHomePage(searchText);

                new GoogleCloudSearchResultPage(_driver)
                    .SelectSearchResult(searchText);

                GoogleCloudPricingCalculatorPage googleCloudPricingCalculatorPage = new GoogleCloudPricingCalculatorPage(_driver)
                    .WaitForCalculatorPageToLoad()
                    .ChooseNumberOfInstances(numberOfInstances)
                    .ChooseWhatAreInstancesFor(whatAreInstancesFor)
                    .ChooseSeries();


                googleCloudPricingCalculatorPage
                    .ChooseMachineType()
                    .AddGPUs()
                    .AddSSD()
                    .ChooseDatacenterLocation();


                string actualTotalCost = googleCloudPricingCalculatorPage
                    .ChooseCommittedUsage()
                    .AddToEstimate()
                    .GetTotalCost();


                Assert.AreEqual(actualTotalCost, expectedTotalCost);
            }

            [TearDown]
            public void QuitDriver()
            {
                _driver.Quit();
            }
        }
    }
}