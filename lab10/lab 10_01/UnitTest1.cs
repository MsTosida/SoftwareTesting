using OpenQA.Selenium.Edge;

namespace lab_10_01
{
    public class Tests
    {
        [TestFixture]
        public class A1FilterByCityTest
        {
            private A1Page a1Page;

            [SetUp]
            public void Setup()
            {
                var driver = new EdgeDriver();
                a1Page = new A1Page(driver);
                driver.Manage().Window.Maximize();
                a1Page.GoToPage();
            }

            [Test]
            public void TestAvailabilityInVitebsk()
            {
                a1Page.CloseCookieBanner();
                a1Page.ClickAvailabilityButton();
                a1Page.SelectCity("�������");
                bool allContainVitebsk = a1Page.AreAllAddressesContainingCity("�������");
                Assert.IsTrue(allContainVitebsk, "�������� �� ������ �. ������� �� ������������");
            }

            [TearDown]
            public void TearDown()
            {
                a1Page.Close();
            }
        }
    }
}