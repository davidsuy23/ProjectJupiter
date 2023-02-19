using NUnit.Framework;
using OpenQA.Selenium;

namespace TestProjectJupiter
{
    public class TestCase3 : TestBase
    {
        [Test]
        [Category("CICD")]
        public void Test3()
        {
            var expectedCostOfFrog = "$10.99";
            var expectedCostOfBunny = "$9.99";
            var expectedCostOfBear = "$14.99";
            var expectedSubTotalFrog = "$21.98";
            var expectedSubTotalBunny = "$49.95";
            var expectedSubTotalBear = "$44.97";
            var expectedTotal = "116.90";

            this.wait.Until(driver => driver.FindElement(By.LinkText("Start Shopping »")));
            this.NavigateToStartShopping();
            // Purchase 2 Stuffed Frogs
            var i = 0;
            while(i != 2)
            {
                this.BuyStuffedFrog();
                i++;
            }
            // Purchase 5 Fluffly Bunnys
            i = 0;
            while (i != 5)
            {
                this.BuyFluffyBunny();
                i++;
            }
            // Purchase 3 Valentine Bears
            i = 0;
            while (i != 3)
            {
                this.BuyValentineBear();
                i++;
            }

            this.NavigateToCart();
            var costOfFrog = this.driver.FindElement(By.XPath("/html/body/div[2]/div/form/table/tbody/tr[1]/td[2]")).Text;
            var costOfBunny = this.driver.FindElement(By.XPath("/html/body/div[2]/div/form/table/tbody/tr[2]/td[2]")).Text;
            var costOfBear = this.driver.FindElement(By.XPath("/html/body/div[2]/div/form/table/tbody/tr[3]/td[2]")).Text;
            
            //Verify the price of each product
            Assert.IsTrue((costOfFrog.Equals(expectedCostOfFrog)), "Cost of Frog should be " + expectedCostOfFrog + " but was " + costOfFrog);
            Assert.IsTrue((costOfBunny.Equals(expectedCostOfBunny)), "Cost of Bunny should be " + expectedCostOfBunny + " but was " + costOfBunny);
            Assert.IsTrue((costOfBear.Equals(expectedCostOfBear)), "Cost of Bear should be " + expectedCostOfBear + " but was " + costOfBear);

            var subTotalFrog = this.driver.FindElement(By.XPath("/html/body/div[2]/div/form/table/tbody/tr[1]/td[4]")).Text;
            var subTotalBunny = this.driver.FindElement(By.XPath("/html/body/div[2]/div/form/table/tbody/tr[2]/td[4]")).Text;
            var subTotalBear = this.driver.FindElement(By.XPath("/html/body/div[2]/div/form/table/tbody/tr[3]/td[4]")).Text;
            
            // Verify Subtotal amount is correct
            Assert.IsTrue((subTotalFrog.Equals(expectedSubTotalFrog)), "Subtotal of Frog should be " + expectedSubTotalFrog + " but was " + subTotalFrog);
            Assert.IsTrue((subTotalBunny.Equals(expectedSubTotalBunny)), "Subtotal of Bunny should be " + expectedSubTotalBunny + " but was " + subTotalBunny);
            Assert.IsTrue((subTotalBear.Equals(expectedSubTotalBear)), "Subtotal of Bear should be " + expectedSubTotalBear + " but was " + subTotalBear);

            //Verify Total
            var total = this.driver.FindElement(By.XPath("/html/body/div[2]/div/form/table/tfoot/tr[1]/td/strong")).Text;
            // Remove the text Total: from the value
            string textToTrim = "Total: ";
            string trimmedTotal = total.TrimStart(textToTrim.ToCharArray()) + "0";
            Assert.IsTrue((trimmedTotal.Equals(expectedTotal)), "Total should be " + expectedTotal + " but was " + trimmedTotal);

            // Verify Sum of Subtotal = Total.
            // Convert strings to decimals, so we can add them up.
            var totalSumSubTotal = decimal.Parse(subTotalBear, System.Globalization.NumberStyles.Currency) + decimal.Parse(subTotalBunny, System.Globalization.NumberStyles.Currency) + decimal.Parse(subTotalFrog, System.Globalization.NumberStyles.Currency);
            // Coverted the decimal back to string for the assert
            Assert.IsTrue((totalSumSubTotal.ToString()).Equals(trimmedTotal));
        }
    }
}