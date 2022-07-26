using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingTask
{
    class SearchProduct
    {
        IWebDriver driver;

        [SetUp]
        public void startBrowser()
        {
            driver = new ChromeDriver("C:\\Program Files\\ChromeTest\\chromedriver");
        }

        [Test]
        public void test()
        {
            driver.Navigate().GoToUrl("http://www.google.co.in");
        }

        [TearDown]
        public void closeBrowser()
        {
            driver.Close();
        }
    }
}
