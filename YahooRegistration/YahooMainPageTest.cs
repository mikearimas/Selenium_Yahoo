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
    public class YahooMainPageTest : PageTestBase
    {
        [Test]
        public void VerifyHeader()
        {
            UITest(() =>
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
                    Assert.NotNull(req);
                    var response = (HttpWebResponse)req.GetResponse();
                    Console.WriteLine(("URL: " + x.GetAttribute("href") + " --- Status: " + response.StatusCode));


                }
            });

        }

        [Test]
        public void VerifyTrending()
        {
            UITest(() =>
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
                    Assert.NotNull(req);
                    var response = (HttpWebResponse)req.GetResponse();
                    Console.WriteLine(("URL: " + x.GetAttribute("href") + " --- Status: " + response.StatusCode));
                }


            });
        }

        [SetUp]
        public void GoToYahoo()
        {

            destinationURL = "http://yahoo.com";
            driver = PageTestBase.GetChromeSetup();
        }
        [TearDown]
        public void TearDownTest()
        {
            driver.Close();
        }
    }
}
