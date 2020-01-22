using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using System;
using OpenQA.Selenium.Support.UI;

namespace YahooRegistration
{

    [TestFixture]
    public class YahooRegistrationTest : PageTestBase
    {

        [TestCaseSource(typeof(RegistrationDetails))]
        public void Register(string firstName, string lastName, string password, string email, string phoneNum, string bMonth, string bDay, string bYear, string gender)
        {
            UITest(() => { 
            driver.Url = destinationURL;
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Manage().Cookies.DeleteAllCookies();

            driver.FindElement(By.XPath("//*[@id='signin-main']/div[1]/a[1]")).Click();
            driver.FindElement(By.Id("usernamereg-firstName")).SendKeys(firstName);
            driver.FindElement(By.XPath("//*[@id='usernamereg-lastName']")).SendKeys(lastName);
            driver.FindElement(By.XPath("//*[@id='usernamereg-yid']")).SendKeys(email);
            driver.FindElement(By.Id("usernamereg-password")).SendKeys(password);
            driver.FindElement(By.XPath("//*[@id='usernamereg-phone']")).SendKeys(phoneNum);

            var monthEle = new SelectElement(driver.FindElement(By.XPath("//*[@id='usernamereg-month']")));
            monthEle.SelectByText(bMonth);

            driver.FindElement(By.XPath("//*[@id='usernamereg-day']")).SendKeys(bDay);
            driver.FindElement(By.XPath("//*[@id='usernamereg-year']")).SendKeys(bYear);
            driver.FindElement(By.XPath("//*[@id='usernamereg-freeformGender']")).SendKeys(gender);

            driver.FindElement(By.XPath("//*[@id='reg-submit-button']")).Click();

                //cannot verify phone number, next page asks to verify phone number which we cannot do
                Assert.IsTrue(driver.PageSource.Contains("recaptcha-iframe"));
                //  Assert.IsTrue(driver.PageSource.Contains("account-challenge-phone-verify"));
                //  TODO: If (recaptha-iframe exists || account-challenge-phone-verify exist) { do something }
                Console.WriteLine("This Account can be made");

    
            
            });

        }

        [SetUp]
        public void GoToYahooMaill()
        {
            destinationURL = "http://mail.yahoo.com";
            driver = PageTestBase.GetChromeSetup();
        }
        [TearDown]
        public void TearDownTest()
        {
            driver.Close();
        }
    }
}
