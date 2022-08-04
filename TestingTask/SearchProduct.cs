using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace TestingTask
{
    public class SearchProduct
    {
        IWebDriver driver;
        WebDriverWait wait;

        [SetUp]
        public void StartBrowser()
        {
            driver = new ChromeDriver("C:\\Program Files\\ChromeTest");
        }

        [Test]
        public void SearchProductTest()
        {
            // Navigate to web site
            driver.Navigate().GoToUrl("http://automationpractice.com/index.php");

            // Wait for Search Box and  magnifier elements are visible on the page
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            wait.Until(ExpectedConditions.ElementExists(By.Id("searchbox")));
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector("button[name*='submit'][name*='search']")));

            // Click Search input and search for "dress"
            driver.FindElement(By.CssSelector("input[id*='search'][id*='query']")).Click();
            driver.FindElement(By.CssSelector("input[id*='search'][id*='query']")).SendKeys("dress");
            driver.FindElement(By.CssSelector("button[name*='submit'][name*='search']")).Click();

            // Check if search input value is "dress" and page responses to search value
            var searchValue = driver.FindElement(By.CssSelector("input[id*='search'][id*='query']")).GetAttribute("value");
            Assert.AreEqual("dress", searchValue, "Search value is nor equal dress");

            // Check if product listing result contains search word "dress"
            var productListingSearchWord = driver.FindElement(By.CssSelector("h1[class*='page'][class*='heading'] span[class='lighter']")).Text.Trim('"');
            Assert.AreEqual("DRESS", productListingSearchWord, "Search value is nor equal dress");

            // Count products on the page and collect product`s titles 
            int productsOnThePage = driver.FindElements(By.XPath("//ul[contains(@class,'product') and contains(@class,'list') and contains(@class,'grid')]//a[contains(@class,'name')]")).Count;
            List<string> titles = new List<string>();

            for (int i = 1; i < productsOnThePage; i++)
            {
                var productTitle = driver.FindElement(By.XPath($"(//ul[contains(@class,'product') and contains(@class,'list') and contains(@class,'grid')]//a[contains(@class,'name')])[{i}]")).GetAttribute("title");
                Assert.IsNotNull(productTitle, $"product title {i} is empty");
                titles.Add(productTitle);

            }

            // Check if all Titles have search work "Dress"
            var searchWord = "Dress";
            List<string> titlesWithoutSearchWord = new List<string>();
            for (int p = 0; p < titles.Count; p++)
            {
                if (!titles[p].Contains(searchWord))
                {
                    titlesWithoutSearchWord.Add(titles[p]);
                }

            }

            // Trow exception if the list with failed condition is not empty
            if (titlesWithoutSearchWord != null)
            {
                throw new InvalidOperationException($"There are titles without search word Dress");
            }
        }

        [TearDown]
        public void CloseBrowser()
        {
            driver.Quit();
        }
    }
}
