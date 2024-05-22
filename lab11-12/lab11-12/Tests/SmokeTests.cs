using Lab11_12.Pages;

namespace Lab11_12.Tests
{
    [TestFixture]
    public class Tests : Steps.Steps
    {
        private const string SEARCH_CITY = "Витебск";
        private List<string> SEARCH_MODEL = new Model.ListModels("Apple").Models;
        private List<string> SEARCH_MODELS = new Model.ListModels("Apple", "Samsung").Models;
        private const string PRICE_FROM = "600";
        private const string PRICE_TO = "1500";
        private const int NUMBER_OF_PRODUCTS_TO_COMPARE = 2;
        private const int NUMBER_OF_PRODUCTS_TO_COMPARE_NEGATIVE_TEST = 5;
        private const int NUMBER_OF_PRODUCTS_TO_COMPARE_MAX = 3;
        private const int INCREASE_PRICE_BY = 3;
        private string SEARCH_TEXT = "64 GB";

        [Test]
        public void TestAvailabilityInVitebsk()
        {
            bool allAdressesContainCity = new A1ProductPage(driver)
                .GoToPage()
                .CloseCookieBanner()
                .ClickAvailabilityButton()
                .SelectCity(SEARCH_CITY)
                .AreAllAddressesContainingCity(SEARCH_CITY);

            Assert.IsTrue(allAdressesContainCity, $"Магазины по адресу г. {SEARCH_CITY} не отобразились");
        }

        [Test]
        public void TestFilterByModelApple()
        {
            bool allContainModel = new A1AllProductsPage(driver)
                .GoToPage()
                .CloseCookieBanner()
                .SetFilterModels(SEARCH_MODEL)
                .AreAllProductsContainingModels(SEARCH_MODEL);
            Assert.IsTrue(allContainModel, $"Фильтр по модели {SEARCH_MODEL} не применился корректно");
        }

        [Test]
        public void TestFilterByModelAppleAndSamsung()
        {
            bool allContainModel = new A1AllProductsPage(driver)
                .GoToPage()
                .CloseCookieBanner()
                .SetFilterModels(SEARCH_MODELS)
                .AreAllProductsContainingModels(SEARCH_MODELS);
            Assert.IsTrue(allContainModel, $"Фильтр по моделям {SEARCH_MODEL} не применился корректно");
        }

        [Test]
        public void TestFilterByPrice()
        {
            bool allContainRightPrice = new A1AllProductsPage(driver)
                .GoToPage()
                .CloseCookieBanner()
                .SetFilterPrice(PRICE_FROM, PRICE_TO)
                .AreAllProductContainingPriceInRange(float.Parse(PRICE_FROM), float.Parse(PRICE_TO));
            Assert.IsTrue(allContainRightPrice, "Фильтр по цене не применился корректно");
        }


        [Test]
        public void TestAddToComarisingLessThatThree()
        {
            A1AllProductsPage productsPage = new A1AllProductsPage(driver);
            List<string> names = productsPage
                .GoToPage()
                .CloseCookieBanner()
                .SelectProductsToComparising(NUMBER_OF_PRODUCTS_TO_COMPARE);
            bool areSelectedProductsInComparising = productsPage.AreSelectedProductsInComparising(names);
            Assert.IsTrue(areSelectedProductsInComparising, "Не все товары добавились в сравнение");
        }

        [Test]
        public void TestAddToComarisingMoreThanThree()
        {
            A1AllProductsPage productsPage = new A1AllProductsPage(driver);
            List<string> names = productsPage
                .GoToPage()
                .CloseCookieBanner()
                .SelectProductsToComparising(NUMBER_OF_PRODUCTS_TO_COMPARE_NEGATIVE_TEST);
            bool areSelectedProductsInComparising = productsPage.AreSelectedProductsInComparising(names.Take(NUMBER_OF_PRODUCTS_TO_COMPARE_MAX).ToList());
            Assert.IsTrue(areSelectedProductsInComparising, "Не все товары добавились в сравнение");
        }

        [Test]
        public void TestChangeProductQuantity()
        {
            A1SmallPriceProductPage productPage = new A1SmallPriceProductPage(driver);
            float initalPrice = productPage
                .GoToPage()
                .CloseCookieBanner()
                .GetInitialProductPrice();

            bool priceChangedCorrectly = productPage.IncreaseQuantity(INCREASE_PRICE_BY).IsPriceChangedCorrectly(initalPrice, INCREASE_PRICE_BY);
            //Assert.IsTrue(priceChangedCorrectly, "Цена не изменилась/изменилась неправильно");
        }


        [Test]
        public void TestAddToCart()
        {
            A1SmallPriceProductPage productPage = new A1SmallPriceProductPage(driver);
            float initalPrice = productPage
                .GoToPage()
                .CloseCookieBanner()
                .GetInitialProductPrice();

            string productName = productPage.GetIntialProductName();

            int initialQuantity = productPage
                .IncreaseQuantity(INCREASE_PRICE_BY).GetInitialProductQuantity();

            productPage.AddToCart();

            bool isProductInCart = productPage.IsProductInCart(productName, initialQuantity, initalPrice);

            Assert.IsTrue(isProductInCart, "Товар не добавился в корзину");
        }

        [Test]
        public void TestDeleteFromCart()
        {
            A1SmallPriceProductPage productPage = new A1SmallPriceProductPage(driver);
            productPage
                .GoToPage()
                .CloseCookieBanner()
                .AddToCart()
                .DeleteFromCart();
                

            //Thread.Sleep(3000);

            //Assert.IsTrue(isCartEmpty, "Товар не удалился из корзины");
        }

        [Test]
        public void TestSearchText()
        {
            A1HomePage homePage = new A1HomePage(driver);

            bool areProductsContainSearchText = homePage 
                .GoToPage()
                .CloseCookieBanner()
                .SearchTextForProducts(SEARCH_TEXT)
                .AreProductsContainSearchText(SEARCH_TEXT);


            Assert.IsTrue(areProductsContainSearchText, "Не все товары содержат текст поиска");
        }
    }
}