using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using System;
using System.Threading;

namespace NetworkOfShopsTest
{
    public class Tests
    {
        public IWebDriver Driver;
        [SetUp]
        public void Setup()
        {
            Driver = new EdgeDriver("C:\\Users\\adi\\Documents\\edgedriver_win32");
        }

        [Test]
        public void Test1()
        {
            Driver.Navigate().GoToUrl("https://localhost:7032/Identity/Account/Login");
            Driver.FindElement(By.Id("Input_Email")).SendKeys("staff@test.pl");
            Driver.FindElement(By.Id("Input_Password")).SendKeys("Pass4Staff!");
            Driver.FindElement(By.Id("login-submit")).Click();
            var currentURL = Driver.Url;
            if (currentURL == "https://localhost:7032/")
                Assert.Pass();
            else
                Assert.Fail();
        }
    }
}