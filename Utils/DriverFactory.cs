using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace CloudQA.Tests.Utils
{
    public static class DriverFactory
    {
        public static IWebDriver CreateChromeDriver()
        {
            new DriverManager().SetUpDriver(new ChromeConfig());
            return new ChromeDriver();
        }
    }
}
