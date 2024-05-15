using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;

namespace GoogleCloudTests
{
    public class GoogleCloudPricingCalculatorPage
    {
        private const int WAIT_TIMEOUT_SECONDS = 25;
        private IWebDriver _driver;

        [FindsBy(How = How.XPath, Using = "//iframe[contains(@name,'goog_')]")]
        private IWebElement calculatorFrame;

        [FindsBy(How = How.XPath, Using = "//input[@ng-model='listingCtrl.computeServer.quantity']")]
        private IWebElement inputNumberOfInstances;

        [FindsBy(How = How.XPath, Using = "//input[@ng-model='listingCtrl.computeServer.label']")]
        private IWebElement inputWhatAreInstancesFor;

        [FindsBy(How = How.XPath, Using = "//md-select[@ng-model='listingCtrl.computeServer.series']")]
        private IWebElement selectSeries;

        [FindsBy(How = How.XPath, Using = "//md-option[@value='n1']")]
        private IWebElement optionN1Series;

        [FindsBy(How = How.XPath, Using = "//md-select[@ng-model='listingCtrl.computeServer.instance']")]
        private IWebElement selectMachineType;

        [FindsBy(How = How.XPath, Using = "//md-option[@value='CP-COMPUTEENGINE-VMIMAGE-N1-STANDARD-8']")]
        private IWebElement optionN2Standard8Machine;

        [FindsBy(How = How.XPath, Using = "//md-checkbox[@ng-model='listingCtrl.computeServer.addGPUs']")]
        private IWebElement checkBoxAddGPUs;

        [FindsBy(How = How.XPath, Using = "//md-select[@ng-model='listingCtrl.computeServer.gpuType']")]
        private IWebElement selectGPUType;

        [FindsBy(How = How.XPath, Using = "//md-option[@value='NVIDIA_TESLA_V100']")]
        private IWebElement optionGPUTeslaV100;

        [FindsBy(How = How.XPath, Using = "//md-select[@ng-model='listingCtrl.computeServer.gpuCount']")]
        private IWebElement selectNumberOFGPUs;

        [FindsBy(How = How.XPath, Using = "//md-option[@ng-disabled='item.value != 0 && item.value < listingCtrl.minGPU' and @value='1']")]
        private IWebElement option1GPU;

        [FindsBy(How = How.XPath, Using = "//md-select[@ng-model='listingCtrl.computeServer.ssd']")]
        private IWebElement selectLocalSSD;

        [FindsBy(How = How.XPath, Using = "//md-option[@ng-repeat='item in listingCtrl.dynamicSsd.computeServer' and @value='2']")]
        private IWebElement option2x375GBLocalSSD;

        [FindsBy(How = How.XPath, Using = "//md-select[@ng-model='listingCtrl.computeServer.location']")]
        private IWebElement selectDatacenterLocation;

        [FindsBy(How = How.XPath, Using = "//md-option[@ng-repeat='item in listingCtrl.fullRegionList | filter:listingCtrl.inputRegionText.computeServer' and @value='europe-west3']")]
        private IWebElement optionFrankfurtLocation;

        [FindsBy(How = How.XPath, Using = "//md-select[@ng-model='listingCtrl.computeServer.cud']")]
        private IWebElement selectCommittedUsage;

        [FindsBy(How = How.XPath, Using = "//div[@class='md-select-menu-container md-active md-clickable']/md-select-menu/md-content/md-option[@value='1' and @ng-value='1']/div[text()='1 Year']")]
        private IWebElement option1YearUsage;

        [FindsBy(How = How.XPath, Using = "//button[@ng-click='listingCtrl.addComputeServer(ComputeEngineForm);']")]
        private IWebElement buttonAddToEstimate;

        [FindsBy(How = How.XPath, Using = "//h2[@class='md-title']/b[@class='ng-binding']")]
        private IWebElement totalEstimatedCost;

        [FindsBy(How = How.Id, Using = "Email Estimate")]
        private IWebElement buttonEmailEstimate;

        [FindsBy(How = How.XPath, Using = "//input[@ng-model='emailQuote.user.email']")]
        private IWebElement inputEmail;

        [FindsBy(How = How.XPath, Using = "//form/md-dialog-actions/button[2]")]
        private IWebElement sendEmailButton;

        public GoogleCloudPricingCalculatorPage(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        public GoogleCloudPricingCalculatorPage WaitForCalculatorPageToLoad()
        {
            WebDriverWait waitForCalculatorTest = new WebDriverWait(_driver, TimeSpan.FromSeconds(WAIT_TIMEOUT_SECONDS));
            waitForCalculatorTest.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("//iframe[contains(@name,'goog_')]")));
            _driver.SwitchTo().Frame(calculatorFrame);
            _driver.SwitchTo().Frame("myFrame");
            return this;
        }

        public GoogleCloudPricingCalculatorPage ChooseNumberOfInstances(int numberOfInstances)
        {
            inputNumberOfInstances.Click();
            inputNumberOfInstances.SendKeys(numberOfInstances.ToString());
            return this;
        }

        public GoogleCloudPricingCalculatorPage ChooseWhatAreInstancesFor(String whatAreInstancesForText)
        {
            inputWhatAreInstancesFor.Click();
            inputWhatAreInstancesFor.SendKeys(whatAreInstancesForText);
            return this;
        }

        public GoogleCloudPricingCalculatorPage ChooseSeries()
        {
            selectSeries.Click();
            optionN1Series = WaitForElementLocatedBy(_driver, By.XPath("//md-option[@value='n1']"));
            optionN1Series.Click();
            return this;
        }

        public GoogleCloudPricingCalculatorPage ChooseMachineType()
        {
            selectMachineType.Click();
            optionN2Standard8Machine = WaitForElementLocatedBy(_driver, By.XPath("//md-option[@value='CP-COMPUTEENGINE-VMIMAGE-N1-STANDARD-8']"));
            optionN2Standard8Machine.Click();
            return this;
        }

        public GoogleCloudPricingCalculatorPage AddGPUs()
        {
            checkBoxAddGPUs.Click();
            selectGPUType = WaitForElementLocatedBy(_driver, By.XPath("//md-select[@ng-model='listingCtrl.computeServer.gpuType']"));
            selectGPUType.Click();
            optionGPUTeslaV100 = WaitForElementLocatedBy(_driver, By.XPath("//md-option[@value='NVIDIA_TESLA_V100']"));
            optionGPUTeslaV100.Click();
            selectNumberOFGPUs.Click();
            option1GPU = WaitForElementLocatedBy(_driver, By.XPath("//md-option[@ng-disabled='item.value != 0 && item.value < listingCtrl.minGPU' and @value='1']"));
            option1GPU.Click();
            return this;
        }

        public GoogleCloudPricingCalculatorPage AddSSD()
        {
            selectLocalSSD.Click();
            option2x375GBLocalSSD = WaitForElementLocatedBy(_driver, By.XPath("//md-option[@ng-repeat='item in listingCtrl.dynamicSsd.computeServer' and @value='2']"));
            option2x375GBLocalSSD.Click();
            return this;
        }

        public GoogleCloudPricingCalculatorPage ChooseDatacenterLocation()
        {
            selectDatacenterLocation.Click();
            optionFrankfurtLocation = WaitForElementLocatedBy(_driver, By.XPath
                    ("//md-option[@ng-repeat='item in listingCtrl.fullRegionList | " +
                            "filter:listingCtrl.inputRegionText.computeServer' and @value='europe-west3']"));
            optionFrankfurtLocation.Click();
            return this;
        }

        public GoogleCloudPricingCalculatorPage ChooseCommittedUsage()
        {
            selectCommittedUsage.Click();
            option1YearUsage = WaitForElementLocatedBy(_driver, By.XPath
                    ("//div[@class='md-select-menu-container md-active md-clickable']/md-select-menu/" +
                            "md-content/md-option[@value='1' and @ng-value='1']/div[text()='1 Year']"));
            option1YearUsage.Click();
            return this;
        }

        public GoogleCloudPricingCalculatorPage AddToEstimate()
        {
            buttonAddToEstimate.Click();
            return this;
        }

        public string GetTotalCost()
        {
            totalEstimatedCost = WaitForElementLocatedBy(_driver, By.XPath("//h2[@class='md-title']/b[@class='ng-binding']"));
            return totalEstimatedCost.Text;
        }

        public GoogleCloudPricingCalculatorPage GetTotalCostOnEmail(string userEmail)
        {
            buttonEmailEstimate.Click();
            inputEmail = WaitForElementLocatedBy(_driver, By.XPath("//input[@ng-model='emailQuote.user.email']"));
            inputEmail.SendKeys(userEmail);
            sendEmailButton.Click();
            return this;
        }


        private static IWebElement WaitForElementLocatedBy(IWebDriver driver, By by)
        {
            return new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                    .Until(ExpectedConditions.ElementIsVisible(by));
        }
    }
}
