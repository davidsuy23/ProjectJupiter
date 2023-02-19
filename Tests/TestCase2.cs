using NUnit.Framework;
using OpenQA.Selenium;

namespace TestProjectJupiter
{
    public class TestCase2 : TestBase
    {
        [Test]
        [Category("CICD")]
        public void Test2()
        {
            this.wait.Until(driver => driver.FindElement(By.LinkText("Contact")));
            this.NavigateToContact();
            this.EnterFirstName(Constants.FirstName);
            this.EnterEmail(Constants.Email);
            this.EnterMessage(Constants.Message);
            this.ClickSubmit();
            this.wait.Until(driver => driver.FindElement(By.LinkText("« Back")));
            var checkBannerText = driver.FindElement(By.XPath("/html/body/div[2]/div/div")).Text;
            //Verify successful submission message appears
            Assert.IsTrue(checkBannerText.Equals("Thanks David, we appreciate your feedback."), checkBannerText + "instead of 'Thanks David, we appreaciate your feedback'");
        }
    }
}