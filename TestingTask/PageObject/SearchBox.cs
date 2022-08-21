using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingTask.PageObejects
{
    public class SearchBox
    {
        IWebDriver driver;
        WebDriverWait wait;
        public SearchBox(IWebDriver driver)
        { 
            this.driver = driver;
            PageFactory.InitElements(driver,this);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
        }

        [FindsBy(How=How.CssSelector, Using = "input[id*='search'][id*='query']")]
        public IWebElement SearchTextbox { get; set; }

        [FindsBy(How=How.CssSelector, Using = "button[name*='submit'][name*='search']")]
        public IWebElement SearchButton { get; set; }

        public void NavigateToResultPage(string SearchWord)
        {
            //wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            wait.Until(driver=>SearchTextbox.Displayed);
            wait.Until(driver => SearchButton.Displayed);
            SearchTextbox.SendKeys(SearchWord);
            SearchButton.Click();
        }
    }
}
