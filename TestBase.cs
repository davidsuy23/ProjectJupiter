using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace TestProjectJupiter
{
    public class TestBase
    {

        public IWebDriver driver;
        public WebDriverWait wait;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl(Constants.WebsiteUrl);
            driver.Manage().Window.Maximize();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
        }

        public void ClickSubmit()
        {
            driver.FindElement(By.LinkText("Submit")).Click();
        }

        public void NavigateToContact()
        {
            driver.FindElement(By.LinkText("Contact")).Click();
            wait.Until(driver => driver.FindElement(By.Id("forename")));
        }

        public void NavigateToStartShopping()
        {
            driver.FindElement(By.LinkText("Start Shopping »")).Click();
            wait.Until(driver => driver.FindElement(By.LinkText("Buy")));
        }

        public void NavigateToCart()
        {
            driver.FindElement(By.CssSelector("#nav-cart > a")).Click();
            wait.Until(driver => driver.FindElement(By.LinkText("Check Out")));
        }

        public void EnterFirstName(string firstName)
        {
            driver.FindElement(By.Id("forename")).SendKeys(firstName);
        }

        public void EnterSurname(string surname)
        {
            driver.FindElement(By.Id("surname")).SendKeys(surname);
        }

        public void EnterEmail(string email)
        {
            driver.FindElement(By.Id("email")).SendKeys(email);
        }

        public void EnterMessage(string message)
        {
            driver.FindElement(By.Id("message")).SendKeys(message);
        }

        public void EnterTelephone(string telephone)
        {
            driver.FindElement(By.Id("telephone")).SendKeys(telephone);
        }

        public void BuyStuffedFrog()
        {
            driver.FindElement(By.CssSelector("#product-2 > div > p > a")).Click();
        }

        public void BuyFluffyBunny()
        {
            driver.FindElement(By.CssSelector("#product-4 > div > p > a")).Click();
        }

        public void BuyValentineBear()
        {
            driver.FindElement(By.CssSelector("#product-7 > div > p > a")).Click();
        }

        [TearDown]
        public void CloseBrowser()
        {
            driver.Close();
        }
    }
}
