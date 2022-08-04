using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace TestingTask
{
    public class Registration
    {
        IWebDriver? driver;
        WebDriverWait wait;

        [SetUp]
        public void StartBrowser()
        {
            driver = new ChromeDriver("C:\\Program Files\\ChromeTest");
        }

        [Test]
        public void RegistrationTest()
        {
            // Navigate to web site
            driver.Navigate().GoToUrl("http://automationpractice.com/index.php");

            // Wait for Sign In button is visible and navigate to Registration section
            WebDriverWait w = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector("[class*='user'][class*='info']")));
            driver.FindElement(By.CssSelector("[class*='user'][class*='info']")).Click();
            wait.Until(ExpectedConditions.ElementExists(By.Id("create-account_form")));

            // Input Email address, click on "Create an account" button and wait for registration form
            driver.FindElement(By.Id("email_create")).Click();
            Random randomEmail = new Random();
            int randomInt = randomEmail.Next(1000);
            driver.FindElement(By.Id("email_create")).SendKeys("username" + randomInt + "@gmail.com");
            driver.FindElement(By.Id("SubmitCreate")).Click();
            wait.Until(ExpectedConditions.ElementExists(By.Id("account-creation_form")));

            // Fill out contact us form: Subject, Email, Referance, File

            // Select element drop down from Sunbject Heading
            driver.FindElement(By.Id("uniform-id_contact")).Click();
            new SelectElement(driver.FindElement(By.CssSelector("[id='id_contact'] option"))).SelectByValue("2");


            // Input Order referance
            driver.FindElement(By.Id("id_order")).SendKeys("1");

            // Ateach the file
            string myScreenShot = "C:\\Users\\AMykhailets\\OneDrive - Oriflame Cosmetics\\Desktop\\MyFileTest.png";
            driver.FindElement(By.Id("fileUpload")).SendKeys(myScreenShot);
            wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id='uniform-fileUpload']//span[@class='filename'][contains(.,'MyFileTest')]")));

            // Input the massege text
            driver.FindElement(By.Id("message")).Click();
            driver.FindElement(By.Id("message")).SendKeys("My Testig Message");

            // Confirm the message by Submit Button and wait for Success message
            driver.FindElement(By.Id("submitMessage")).Click();
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector("p[class*='alert'][class*='success']")));
        }

        [TearDown]
        public void CloseBrowser()
        {
            driver.Quit();
        }
    }
}
