using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.UI;
using System.Threading;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;

namespace Unitest_Test6
{
    class SeleniumDemo
    {
        IWebDriver driver;
        ExtentReports extent;
        ExtentTest test;

        [SetUp]
        public void startBrowser()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
        }

        [OneTimeSetUp]
        public void SetupReporting()
        {
            var htmlReporter = new ExtentHtmlReporter(@"C:\Users\84332\Desktop\report.html"); // Thay đổi đường dẫn đến nơi bạn muốn lưu trữ Test Result File
            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);
        }

        private IWebElement WaitUntilElementExists(By by, int timeoutInSeconds)
        {
            for (int i = 0; i < timeoutInSeconds; i++)
            {
                try
                {
                    IWebElement element = driver.FindElement(by);
                    if (element != null)
                    {
                        return element;
                    }
                }
                catch (NoSuchElementException)
                {
                    // Nếu không tìm thấy phần tử, chờ 1 giây trước khi thử lại
                    Thread.Sleep(1000);
                }
            }
            throw new NoSuchElementException("Element not found");
        }

        [Test]
        public void Demo1()
        {
            driver.Url = "http://localhost:8080/test6/";

            test = extent.CreateTest("Demo1");
            // Sử dụng hàm chờ WaitUntilElementExists để chờ cho phần tử có XPath "//a[normalize-space()='Add/Remove Elements']" xuất hiện
            IWebElement button = driver.FindElement(By.Id("mainframe.WorkFrame.form.Button00")); // Thay "buttonId" bằng ID thực tế của button

            button.Click();
            string buttonValue = button.GetAttribute("value");
            if(buttonValue == "BUTTON CLICK")
            {
                test.Pass("Test Passed");
            }
            else
            {
                test.Pass("Test Fail");
            }
           
        }

        
        

        [TearDown]
        public void closeBrowser()
        {
           /* if (driver != null)
            {
                driver.Quit(); // Đóng trình duyệt Chrome
            }*/
        }

        [OneTimeTearDown]
        public void ReportTeardown()
        {
            extent.Flush(); // Đảm bảo rằng báo cáo được lưu trữ và đóng
        }
    }
}
