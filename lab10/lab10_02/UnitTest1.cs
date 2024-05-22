using OpenQA.Selenium.Edge;

namespace lab10_02
{
    public class Tests
    {
        [TestFixture]
        public class A1FilterTest
        {
            private A1ProductsPage a1Page;

            [SetUp]
            public void Setup()
            {
                var driver = new EdgeDriver();
                a1Page = new A1ProductsPage(driver);
                driver.Manage().Window.Maximize();
                a1Page.GoToPage();
            }

            [Test]
            public void TestFilterByModelApple()
            {
                List<string> models = new List<string>() { "Apple" };
                a1Page.CloseCookieBanner();
                a1Page.SetFilterModels(models);
                bool allContainModel = a1Page.AreAllProductsContainingModels(models);
                Assert.IsTrue(allContainModel, "Фильтр по модели Apple не применился корректно");
                Thread.Sleep(3000);
            }
            [Test]
            public void TestFilterByModelAppleAndSamsung()
            {
                List<string> models = new List<string>() { "Apple", "Samsung" };
                a1Page.CloseCookieBanner();
                a1Page.SetFilterModels(models);
                bool allContainModel = a1Page.AreAllProductsContainingModels(models);
                Assert.IsTrue(allContainModel, "Фильтр по моделям Apple и Samsung не применился корректно");
                Thread.Sleep(3000);
            }

            [Test]
            public void TestFilterByPrice()
            {
                string priceFrom = "600";
                string priceTo = "1500";

                a1Page.CloseCookieBanner();
                a1Page.SetFilterPrice(priceFrom, priceTo);
                bool allContainRightPrice = a1Page.AreAllProductContainingPriceInRange(float.Parse(priceFrom), float.Parse(priceTo));
                Assert.IsTrue(allContainRightPrice, "Фильтр по цене не применился корректно");
                Thread.Sleep(3000);
            }


            [TearDown]
            public void TearDown()
            {
                a1Page.Close();
            }
        }
    }
}