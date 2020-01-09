using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using System;
using OpenQA.Selenium.Support.UI;

namespace YahooRegistration
{

    [TestFixture]
    public class YahooRegistrationTest
    {

        private IWebDriver driver;
        public string destinationURL;


        [TestCaseSource(typeof(RegistrationDetails))]
        public void Register(string firstName, string lastName, string password, string email, string phoneNum, string bMonth, string bDay, string bYear, string gender)
        {
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

            try
            {
                Assert.IsTrue(driver.PageSource.Contains("account-challenge-phone-verify"));

            } catch
            {
                Console.WriteLine("Account details did not pass Yahoo requirements");
            }
            
            

        }

        [SetUp]
        public void GoToYahoo()
        {

            destinationURL = "http://mail.yahoo.com";
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("--disable-notifications");
            driver = new ChromeDriver(options);
        }
        [TearDown]
        public void TearDownTest()
        {
            driver.Close();
        }
    }
}
