using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using CloudQA.Tests.Utils;
using System;
using System.IO;

namespace CloudQA.Tests.Tests
{
    public class CompleteFormTest
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void Setup()
        {
            driver = DriverFactory.CreateChromeDriver();
            driver.Manage().Window.Maximize();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Navigate().GoToUrl("https://app.cloudqa.io/home/AutomationPracticeForm");
        }

        [Test]
        public void FillEntireForm()
        {
            var firstName = wait.Until(ExpectedConditions.ElementIsVisible(
                By.XPath("//label[contains(text(),'First Name')]/following-sibling::input")));
            firstName.SendKeys("Jayanth");

            var lastName = wait.Until(ExpectedConditions.ElementIsVisible(
                By.XPath("//label[contains(text(),'Last Name')]/following-sibling::input")));
            lastName.SendKeys("Thalla");

            var email = wait.Until(ExpectedConditions.ElementIsVisible(
                By.XPath("//label[contains(text(),'Email')]/following-sibling::input")));
            email.SendKeys("jayanth@example.com");

            var male = wait.Until(ExpectedConditions.ElementToBeClickable(
                By.XPath("//label[contains(text(),'Male')]")));
            male.Click();

            var mobile = wait.Until(ExpectedConditions.ElementIsVisible(
                By.XPath("//label[contains(text(),'Mobile')]/following-sibling::input")));
            mobile.SendKeys("9876543210");

            var dob = wait.Until(ExpectedConditions.ElementIsVisible(
                By.Id("dateOfBirthInput")));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].value='05 Dec 2001';", dob);

            var subjects = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("subjectsInput")));
            subjects.SendKeys("Math");
            subjects.SendKeys(Keys.Enter);
            subjects.SendKeys("Physics");
            subjects.SendKeys(Keys.Enter);

            ClickLabel("Sports");
            ClickLabel("Reading");
            ClickLabel("Music");

            string filePath = Path.Combine(TestContext.CurrentContext.WorkDirectory, "sample.png");
            File.WriteAllText(filePath, "dummy");
            driver.FindElement(By.Id("uploadPicture")).SendKeys(filePath);

            driver.FindElement(By.Id("currentAddress")).SendKeys("Hyderabad, India");

            var state = wait.Until(ExpectedConditions.ElementToBeClickable(
                By.XPath("//div[contains(text(),'Select State')]")));
            state.Click();
            ClickDiv("NCR");

            var city = wait.Until(ExpectedConditions.ElementToBeClickable(
                By.XPath("//div[contains(text(),'Select City')]")));
            city.Click();
            ClickDiv("Delhi");

            var submit = driver.FindElement(By.Id("submit"));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", submit);

            var modal = wait.Until(ExpectedConditions.ElementIsVisible(
                By.XPath("//div[contains(@class,'modal-content')]")));
            Assert.IsTrue(modal.Displayed);
        }

        private void ClickLabel(string text)
        {
            var el = wait.Until(ExpectedConditions.ElementToBeClickable(
                By.XPath($"//label[contains(text(),'{text}')]")));
            el.Click();
        }

        private void ClickDiv(string text)
        {
            var el = wait.Until(ExpectedConditions.ElementToBeClickable(
                By.XPath($"//div[contains(text(),'{text}')]")));
            el.Click();
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}
