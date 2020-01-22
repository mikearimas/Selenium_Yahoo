using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.Extensions;
using System;
using System.IO;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace YahooRegistration
{
    public class PageTestBase
    {
        protected static IWebDriver driver;
        public string destinationURL;
        public HttpWebRequest req = null;

        private static ChromeDriver _ChromeDriver;
        private static ChromeOptions _ChromeOptions = new ChromeOptions();
        protected void UITest(Action action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                SetupAndTakeScreenshot();
                throw;
            }
        }
        private static void ChromeSetup()
        {
            _ChromeOptions.AddArgument("--disable-notifications");
            _ChromeDriver = new ChromeDriver(_ChromeOptions);
           
        }
        public static ChromeDriver GetChromeSetup()
        {
            ChromeSetup();
            return _ChromeDriver;
        }

        private static void SetupAndTakeScreenshot()
        {
            var screenshot = driver.TakeScreenshot();
            DateTime curTime = DateTime.Now;
            string incidentTime = "_date_" + curTime.ToString("yyyy-MM-dd") + "_time_" + curTime.ToString("HH-mm-ss");
            string testName = new StackTrace().GetFrame(1).GetMethod().Name;
            string fileName = testName + "_" + incidentTime + ".png";
            string folderPath = CreateScreenshotFolder();
            screenshot.SaveAsFile(folderPath + fileName, ScreenshotImageFormat.Png);
        }

        private static string CreateScreenshotFolder()
        {
            string mainFolder = "E:/YahooTestSS/";
            string subFolder = DateTime.Now.Year + "_" + DateTime.Now.Month + "\\";
            string fullPath = mainFolder + subFolder;
            if (!Directory.Exists(fullPath)) Directory.CreateDirectory(fullPath);
            return fullPath;
        }


    }
}
