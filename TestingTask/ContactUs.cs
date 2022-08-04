﻿using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace TestingTask
{
    public class ContactUs
    {
        IWebDriver? driver;
        WebDriverWait wait;

        [SetUp]
        public void StartBrowser()
        {
            driver = new ChromeDriver("C:\\Program Files\\ChromeTest");
        }

        [Test]
        public void ContactUsTest()
        {
            // Navigate to web site
            driver.Navigate().GoToUrl("http://automationpractice.com/index.php");

            // Wait for Contact us button is visible and navigate to contact us section
            WebDriverWait w = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            wait.Until(ExpectedConditions.ElementExists(By.Id("contact-link")));
            driver.FindElement(By.Id("contact-link")).Click();
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector("form[class*='contact'][class*='box']")));

            // Check elements of contact us form are present
            Assert.IsFalse(string.IsNullOrEmpty(driver.FindElement(By.CssSelector("[id='center_column'] h1[class*='page'][class*='heading']")).Text), "The title of contact us section is empty");
            Assert.IsFalse(string.IsNullOrEmpty(driver.FindElement(By.CssSelector("h3[class*='page'][class*='subheading']")).Text), "The subtitle of contact us form is empty");
            Assert.IsTrue(driver.FindElement(By.CssSelector("form[class*='contact'][class*='box'] div[class='clearfix']")).Displayed, "Contact us form is not visible");

            // Fill out contact us form: Subject, Email, Referance, File
                
                // Select element drop down from Sunbject Heading
                driver.FindElement(By.Id("uniform-id_contact")).Click();
                new SelectElement(driver.FindElement(By.CssSelector("[id='id_contact'] option"))).SelectByValue("2");

                // Input Email
                driver.FindElement(By.Id("email")).Click();
                Random randomEmail = new Random();
                int randomInt = randomEmail.Next(1000);
                driver.FindElement(By.Id("email")).SendKeys("username" + randomInt + "@gmail.com");

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
