using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestingTask
{
    public class SearchProduct
    {
        IWebDriver driver;
        WebDriverWait wait;

        [SetUp]
        public void StartBrowser()
        {
            driver = new ChromeDriver("C:\\Program Files\\ChromeTest\\chromedriver");
        }

        [Test]
        public void Test()
        {
            // Navigate to web site
            driver.Navigate().GoToUrl("http://automationpractice.com/index.php");

            // Wait for Search Box and  magnifier elements are visible on the page
            WebDriverWait w = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            wait.Until(ExpectedConditions.ElementExists(By.Id("searchbox")));
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector("button[name*='submit'][name*='search']")));

            // Click Search input and search for "dress"
            driver.FindElement(By.CssSelector("input[id*='search'][id*='query']")).Click();
            driver.FindElement(By.CssSelector("input[id*='search'][id*='query']")).SendKeys("dress");
            driver.FindElement(By.CssSelector("button[name*='submit'][name*='search']")).Click();

            // Check if search input value is "dress" and page responses to search value
            var searchValue = driver.FindElement(By.CssSelector("button[name*='submit'][name*='search']")).GetAttribute("value");
            Assert.AreEqual("dress", searchValue, "Search value is nor equal dress");

            // Check if product listing result contains search word "dress"
            var productListingSearchWord = driver.FindElement(By.CssSelector("h1[class*='page'][class*='heading'] span[class='lighter']")).Text;
            Assert.AreEqual("dress", productListingSearchWord, "Search value is nor equal dress");

            // Check if the first product in products listing contains search word "Dress"
            var searchWord = "Dress";
            int productsOnThePage = driver.FindElements(By.XPath("//ul[contains(@class,'product') and contains(@class,'list') and contains(@class,'grid')]//a[contains(@class,'name')]")).Count;

            for (int i = 1; i < productsOnThePage; i++)
            {
                driver.FindElement(By.XPath($"(//ul[contains(@class,'product') and contains(@class,'list') and contains(@class,'grid')]//a[contains(@class,'name') and contains(text(),'{searchWord}')])[{i}]"));
            }


            // Clear 

        }

        [TearDown]
        public void CloseBrowser()
        {
            driver.Quit();
        }
    }
}
