using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingTask.Generators
{
    public class PhoneNumberGenerator
    {
        IWebDriver driver;
        public PhoneNumberGenerator(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }
        public string GeneratorPhoneNumber(int length)
        {
            Random random = new Random();
            const string chars = "123456890101112131415161890";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
