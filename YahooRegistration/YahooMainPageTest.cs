using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace YahooRegistration
{
    [TestFixture]
    public class YahooMainPageTest
    {
        private IWebDriver driver;
        public string destinationURL;
        HttpWebRequest req = null;

        [Test]
        public void VerifyHeader()
        {
            driver.Url = destinationURL;
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Manage().Cookies.DeleteAllCookies();

         
            var header = driver.FindElements(By.XPath("//*[@id ='header-nav-bar']/li/a"));

            foreach (var x in header)
            {
                string href = x.GetAttribute("href");
                req = (HttpWebRequest)WebRequest.Create(href);

                try
                {
                    Assert.NotNull(req);
                    var response = (HttpWebResponse)req.GetResponse();
                    Console.WriteLine(("URL: " + x.GetAttribute("href") + " --- Status: " + response.StatusCode));

                }
                catch (WebException e)
                {
                    var errorResponse = (HttpWebResponse)e.Response;
                    Console.WriteLine(("URL: " + x.GetAttribute("href") + " --- Status: " + errorResponse.StatusCode));
                }
                catch(Exception e)
                {
                    Console.WriteLine("Exception caught: " + e);
                }
            }

        }

        [Test]
        public void VerifyTrending()
        {

            driver.Url = destinationURL;
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Manage().Cookies.DeleteAllCookies();


            var header = driver.FindElements(By.XPath("//*[@class ='trending-list']/ul/li/a"));

             foreach (var x in header)
            {
                string href = x.GetAttribute("href");
                req = (HttpWebRequest)WebRequest.Create(href);
                try
                {
                    Assert.NotNull(req);
                    var response = (HttpWebResponse)req.GetResponse();
                    Console.WriteLine(("URL: " + x.GetAttribute("href") + " --- Status: " + response.StatusCode));

                }
                catch (WebException e)
                {
                    var errorResponse = (HttpWebResponse)e.Response;
                    Console.WriteLine(("URL: " + x.GetAttribute("href") + " --- Status: " + errorResponse.StatusCode));
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception caught: " + e);
                }
            }
        }

        [SetUp]
        public void GoToYahoo()
        {

            destinationURL = "http://yahoo.com";
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
