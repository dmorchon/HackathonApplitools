using Applitools;
using Applitools.Selenium;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Hackathon
{
    [TestFixture]
    public class TraditionalTests
    {
        //We need change this URL for test Web V1 or Web V2
        //URL V1
        public static string URL = "https://demo.applitools.com/hackathon.html";
        //URL V2
        //public static string URL = "https://demo.applitools.com/hackathonV2.html";

        private IWebDriver driver;
       
        [OneTimeSetUp]
        public void BeforeEach()
        {
            // Use Chrome browser
            //We need the chromedriver in this file on our PC and we need download chromedriver of our version of chrome (My case is 78)
            driver = new ChromeDriver(@"C:\chromedriver_win32_78");
            driver.Manage().Timeouts().ImplicitWait = new TimeSpan(0, 0, 10);
        }

        /// <summary>
        /// Check the Visual Elements of Login Page
        /// </summary>
        [Test]
        public void Test01LoginVisualElements()
        {
            driver.Manage().Window.Size = new Size(800, 600);
            driver.Url = URL;
            try
            {
                //Check Principal Logo
                driver.FindElement(By.ClassName("logo-w"));
                //Check Login Form Text
                driver.FindElement(By.XPath("//*[contains(text(), 'Login Form')]"));
                //Check Username Text
                driver.FindElement(By.XPath("//*[contains(text(), 'Username')]"));
                //Check Password Text
                driver.FindElement(By.XPath("//*[contains(text(), 'Password')]"));
                //Check Male Icon (Username)
                driver.FindElement(By.XPath("//*[@class='pre-icon os-icon os-icon-user-male-circle']"));
                //Check Password Icon
                driver.FindElement(By.XPath("//*[@class='pre-icon os-icon os-icon-fingerprint']"));
                //Check Username Edit Text
                driver.FindElement(By.Id("username"));
                //Check Password Edit Text
                driver.FindElement(By.Id("password"));
                //Check Log-In Button and Remember Check
                driver.FindElement(By.Id("log-in"));
                //Check the Social Login Buttons
                //Check Twitter Button
                driver.FindElement(By.CssSelector("img[src='img/social-icons/twitter.png']"));
                //Check Facebook Button
                driver.FindElement(By.CssSelector("img[src='img/social-icons/facebook.png']"));
                //Check Linkedin Button
                driver.FindElement(By.CssSelector("img[src='img/social-icons/linkedin.png']"));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(String.Format("Error find Element: {0}", ex.Message));
                Assert.Fail(ex.Message);
            }
            Assert.True(true);
        }

        /// <summary>
        /// Check the Login Dates Cases in V1.
        /// Login without Username and Password
        /// </summary>
        [Test]
        public void Test02LoginDatesWithoutUsermamePassword()
        {
            driver.Manage().Window.Size = new Size(800, 600);
            driver.Url = URL;
            //Test login without Username and Password
            try
            {
                driver.FindElement(By.Id("log-in")).Click();
                IWebElement textError = driver.FindElement(By.XPath("//*[@class='alert alert-warning']"));
                Assert.AreEqual("Both Username and Password must be present", textError.Text);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(String.Format("Error find Element: {0}", ex.Message));
                Assert.Fail(ex.Message);
            }
            Assert.True(true);
        }

        /// <summary>
        /// Login without Password
        /// </summary>
        [Test]
        public void Test03LoginDatesWithoutPassword()
        {
            driver.Manage().Window.Size = new Size(800, 600);
            driver.Url = URL;
            //Test login without Password
            try
            {
                driver.FindElement(By.Id("username")).SendKeys("Test");
                driver.FindElement(By.Id("log-in")).Click();
                IWebElement textError = driver.FindElement(By.XPath("//*[@class='alert alert-warning']"));
                Assert.AreEqual("Password must be present", textError.Text);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(String.Format("Error find Element: {0}", ex.Message));
                Assert.Fail(ex.Message);
            }
            Assert.True(true);
        }

        /// <summary>
        /// Login without Username
        /// </summary>
        [Test]
        public void Test04LoginDatesWithoutUsername()
        {
            driver.Manage().Window.Size = new Size(800, 600);
            driver.Url = URL;
            //Test login without Username
            try
            {
                driver.FindElement(By.Id("password")).SendKeys("Test01");
                driver.FindElement(By.Id("log-in")).Click();
                IWebElement textError = driver.FindElement(By.XPath("//*[@class='alert alert-warning']"));
                Assert.AreEqual("Username must be present", textError.Text);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(String.Format("Error find Element: {0}", ex.Message));
                Assert.Fail(ex.Message);
            }
            Assert.True(true);
        }

        /// <summary>
        /// Login with dates
        /// </summary>
        [Test]
        public void Test05LoginDates()
        {
            driver.Manage().Window.Size = new Size(800, 600);
            driver.Url = URL;
            //Test login dates
            try
            {
                driver.FindElement(By.Id("username")).SendKeys("Test");
                driver.FindElement(By.Id("password")).SendKeys("Test01");
                driver.FindElement(By.Id("log-in")).Click();
                if (URL.Contains("V2"))
                    Assert.AreEqual("https://demo.applitools.com/hackathonAppV2.html", driver.Url);
                else
                    Assert.AreEqual("https://demo.applitools.com/hackathonApp.html", driver.Url);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(String.Format("Error find Element: {0}", ex.Message));
                Assert.Fail(ex.Message);
            }
            Assert.True(true);
        }

        /// <summary>
        /// Check the order of columns
        /// </summary>
        [Test]
        public void Test06AmountsOrder()
        {
            driver.Manage().Window.Size = new Size(800, 600);
            driver.Url = URL;
            try
            {
                //First Login
                driver.FindElement(By.Id("username")).SendKeys("Test");
                driver.FindElement(By.Id("password")).SendKeys("Test01");
                driver.FindElement(By.Id("log-in")).Click();
                //Click on Amount Header
                driver.FindElement(By.Id("amount")).Click();
                //Get All Rows
                IWebElement table = driver.FindElement(By.Id("transactionsTable"));
                IList<IWebElement> AllRows = table.FindElements(By.TagName("tr"));
                //Check the order
                double lastRow = 0;
                for (int i = 1; i < AllRows.Count(); i++)
                {
                    IWebElement row = driver.FindElement(By.XPath(".//*[@id='transactionsTable']/tbody/tr[" + i + "]/td[5]"));
                    var format = new NumberFormatInfo();
                    format.NegativeSign = "-";
                    format.NumberDecimalSeparator = ".";
                    double value = Double.Parse(row.Text.Replace("USD", "").Replace(" ", ""), format);
                    if (i == 1)
                        lastRow = value;
                    if (value < lastRow)
                    {
                        Assert.Fail(String.Format("The order of column isn't correct in row {0}", i));
                    }
                    lastRow = value;
                }
                Assert.IsTrue(true, "All order columns is correct");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(String.Format("Error find Element: {0}", ex.Message));
                Assert.Fail(ex.Message);
            }
            Assert.True(true);
        }

        /// <summary>
        /// Can't automate this test because we don't have access to the data
        /// Check the order of chart
        /// </summary>
        [Test]
        public void Test07CanvasChart()
        {
            driver.Manage().Window.Size = new Size(800, 600);
            driver.Url = URL;
            try
            {
                //First Login
                driver.FindElement(By.Id("username")).SendKeys("Test");
                driver.FindElement(By.Id("password")).SendKeys("Test01");
                driver.FindElement(By.Id("log-in")).Click();
                //Click on Compare Expenses
                driver.FindElement(By.Id("showExpensesChart")).Click();
                /*
                 * We can't automate this test in a traditional way 
                 * because we don't have access to the data inside the canvas
                 * */
            }
            catch (Exception ex)
            {
                Debug.WriteLine(String.Format("Error find Element: {0}", ex.Message));
                Assert.Fail(ex.Message);
            }
            Assert.True(true);
        }

        /// <summary>
        /// Check the gifs on the page
        /// </summary>
        [Test]
        public void Test08DynamicGif()
        {
            driver.Manage().Window.Size = new Size(800, 600);
            driver.Url = URL;
            try
            {
                //First Login
                driver.FindElement(By.Id("username")).SendKeys("Test");
                driver.FindElement(By.Id("password")).SendKeys("Test01");
                driver.FindElement(By.Id("log-in")).Click();
                /*
                 * In a traditional test, we can't check if the gif has
                 * changed, but we can check that element exists
                 * */
                //Check the gifs
                IWebElement flashSaleGif = driver.FindElement(By.Id("flashSale"));
                if (flashSaleGif == null)
                    Assert.Fail("Flash Sale Gif not found");
                IWebElement flashSale2Gif = driver.FindElement(By.Id("flashSale2"));
                if (flashSaleGif == null)
                    Assert.Fail("Flash Sale 2 Gif not found");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(String.Format("Error find Element: {0}", ex.Message));
                Assert.Fail(ex.Message);
            }
            Assert.True(true);
        }

    }
}
