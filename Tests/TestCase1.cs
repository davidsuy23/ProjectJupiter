using NUnit.Framework;
using OpenQA.Selenium;

namespace TestProjectJupiter
{
    public class TestCase1 : TestBase
    {
        [Test]
        [Category("CICD")]
        public void Test1()
        {
            this.wait.Until(driver => driver.FindElement(By.LinkText("Contact")));
            this.NavigateToContact();
            this.ClickSubmit();
            this.wait.Until(driver => driver.FindElement(By.Id("forename-err")));
            
            // verify all required error messages
            var errorValue = driver.FindElement(By.Id("forename-err")).Text;
            Assert.IsTrue(errorValue.Equals("Forename is required"), "should display 'Forename is required'");
            errorValue = driver.FindElement(By.Id("email-err")).Text;
            Assert.IsTrue(errorValue.Equals("Email is required"), "should display 'Email is required'");
            errorValue = driver.FindElement(By.Id("message-err")).Text;
            Assert.IsTrue(errorValue.Equals("Message is required"), "should display 'Message is required'");
            var headerError = driver.FindElement(By.Id("header-message")).Text;
            Assert.IsTrue(headerError.Equals("We welcome your feedback - but we won't get it unless you complete the form correctly."), "should display 'but we wont get it unless you complete the form correctly'");
            
            // verify a invalid email error
            this.EnterEmail("david");
            errorValue = driver.FindElement(By.Id("email-err")).Text;
            Assert.IsTrue(errorValue.Equals("Please enter a valid email"), errorValue + "doesnt display 'Please enter a valid email'");
            
            // Enter data to remove validation errors
            this.EnterFirstName(Constants.FirstName);
            this.EnterSurname(Constants.Surname);
            this.EnterEmail(Constants.Email);
            this.EnterTelephone(Constants.Telephone);
            this.EnterMessage(Constants.Message);
            
            // Verify Banner updated
            headerError = driver.FindElement(By.Id("header-message")).Text;
            Assert.IsTrue(headerError.Contains("tell it how it is"), headerError + "doesnt contain 'tell it how it is'");
            
            // Verify validation errors disappear
            var checkValidControl = driver.FindElement(By.Id("forename")).GetAttribute("class");
            Assert.IsTrue(checkValidControl.Contains("ng-valid"), "Control should not show error message");
            checkValidControl = driver.FindElement(By.Id("email")).GetAttribute("class");
            Assert.IsTrue(checkValidControl.Contains("ng-valid"), "Control should not show error message");
            checkValidControl = driver.FindElement(By.Id("message")).GetAttribute("class");
            Assert.IsTrue(checkValidControl.Contains("ng-valid"), "Control should not show error message");
        }
    }
}