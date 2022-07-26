﻿using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using TestingTask.BaseClass;
using TestingTask.Generators;

namespace TestingTask.TestScripts
{
    public class Registration : TestBase
    {
        WebDriverWait wait;

        [Test]
        public void RegistrationTest()
        {
            // Declare Generators
            var generator1 = new WordGenerator(driver);
            var generator2 = new PhoneNumberGenerator(driver);

            // Wait for Sign In button is visible and navigate to Registration section
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector("[class*='user'][class*='info']")));
            driver.FindElement(By.CssSelector("[class*='user'][class*='info']")).Click();
            wait.Until(ExpectedConditions.ElementExists(By.Id("create-account_form")));

            // Input Email address, click on "Create an account" button and wait for registration form
            driver.FindElement(By.Id("email_create")).Click();
            Random randomEmail = new Random();
            int randomInt = randomEmail.Next(1000);
            var emailAddress = generator1.GenerateWord(12) + randomInt + "@gmail.com";
            driver.FindElement(By.Id("email_create")).SendKeys(emailAddress);
            driver.FindElement(By.Id("SubmitCreate")).Click();
            wait.Until(ExpectedConditions.ElementExists(By.Id("account-creation_form")));

            // Check if the email field is pre-filled
            Assert.AreEqual(emailAddress, driver.FindElement(By.Id("email")).GetAttribute("value"), "Pre-filled email is different or null");

            // Fill out all mandatory fielads in registration form

            // Personal data
            driver.FindElement(By.Id("id_gender2")).Click();
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector("[id='uniform-id_gender2'] span[class='checked']")));
            var firstName = generator1.GenerateWord(6);
            var lastName = generator1.GenerateWord(10);
            driver.FindElement(By.Id("customer_firstname")).SendKeys(firstName);
            driver.FindElement(By.Id("customer_lastname")).SendKeys(lastName);
            driver.FindElement(By.Id("passwd")).SendKeys("Angie12345");

            // Address (Check the First Name and Last name pre-filled fields)
            Assert.AreEqual(firstName, driver.FindElement(By.Id("firstname")).GetAttribute("value"), "Pre-filled First name is different or null");
            Assert.AreEqual(lastName, driver.FindElement(By.Id("lastname")).GetAttribute("value"), "Pre-filled Last name is different or null");
            driver.FindElement(By.Id("address1")).SendKeys("Riegrova");
            driver.FindElement(By.Id("city")).SendKeys("Olomouc");
            driver.FindElement(By.Id("uniform-id_state")).Click();
            driver.FindElement(By.CssSelector("select[id='id_state'] option[value='9']")).Click();
            driver.FindElement(By.Id("postcode")).SendKeys("77900");
            //driver.FindElement(By.Id("phone_mobile")).SendKeys($"+1484{PhoneNumberGenerator(7)}");
            driver.FindElement(By.Id("phone_mobile")).SendKeys("+1484" + generator2.GeneratorPhoneNumber(7));
            driver.FindElement(By.Id("alias")).SendKeys("Default Address");


            // Submit registration form and check if consultant was auto logged in
            driver.FindElement(By.Id("submitAccount")).Click();
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector("[class*='user'][class*='info'] a[class='logout']")));
            Assert.IsTrue(driver.FindElement(By.XPath($"//*[contains(@class,'user') and contains(@class,'info')]/a[contains(@href,'my-account')]/span[contains(.,'{firstName}')]")).Displayed, $"My account Name does not contain {firstName} in the top navigation");
            Assert.IsTrue(driver.FindElement(By.CssSelector("[id='center_column'] [class*='addresses'][class*='lists']")).Displayed, "My account options are not visible on the page");
        }
    }
}
