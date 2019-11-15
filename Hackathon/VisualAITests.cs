using Applitools;
using Applitools.Selenium;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hackathon
{
    [TestFixture]
    public class VisualAITests
    {
        //We need change this URL for test Web V1 or Web V2
        //URL V1
        public static string URL = "https://demo.applitools.com/hackathon.html";
        //URL V2
        //public static string URL = "https://demo.applitools.com/hackathonV2.html";

        private EyesRunner runner;
        private Eyes eyes;

        private IWebDriver driver;
        [SetUp]
        public void BeforeEach()
        {
            //Initialize the Runner for your test.
            runner = new ClassicRunner();

            // Initialize the eyes SDK (IMPORTANT: make sure your API key is set in the APPLITOOLS_API_KEY env variable).
            eyes = new Eyes(runner);
            eyes.ApiKey = "i21cQTDmsqy5Cnqk7sEVZ2PlDgTSef25xbC102w56i107dU110";

            // Use Chrome browser
            //We need the chromedriver in this file on our PC and we need download chromedriver of our version of chrome (My case is 78)
            driver = new ChromeDriver(@"C:\chromedriver_win32_78");
        }

        /// <summary>
        /// Check the Visual Elements of Login Page
        /// </summary>
        [Test]
        public void Test01LoginVisualElements()
        {
            eyes.Open(driver, "Hackathon test", "Visual Test Hackathon", new Size(800, 600));
            driver.Url = URL;
            eyes.CheckWindow("Login Page");
            try
            {
                //Check Principal Logo
                eyes.CheckElement(By.ClassName("logo-w"));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(String.Format("Error find element: {0}", ex.Message));
            }
            try
            {
                //Check Login Form Text
                eyes.CheckElement(By.XPath("//*[contains(text(), 'Login Form')]"));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(String.Format("Error find element: {0}", ex.Message));
            }
            try
            {
                //Check Username Text
                eyes.CheckElement(By.XPath("//*[contains(text(), 'Username')]"));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(String.Format("Error find element: {0}", ex.Message));
            }
            try
            {
                //Check Password Text
                eyes.CheckElement(By.XPath("//*[contains(text(), 'Password')]"));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(String.Format("Error find element: {0}", ex.Message));
            }
            try
            {
                //Check Male Icon (Username)
                eyes.CheckElement(By.XPath("//*[@class='pre-icon os-icon os-icon-user-male-circle']"));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(String.Format("Error find element: {0}", ex.Message));
            }
            try
            {
                //Check Password Icon
                eyes.CheckElement(By.XPath("//*[@class='pre-icon os-icon os-icon-fingerprint']"));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(String.Format("Error find element: {0}", ex.Message));
            }
            try
            {
                //Check Username Edit Text
                eyes.CheckElement(By.Id("username"));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(String.Format("Error find element: {0}", ex.Message));
            }
            try
            {
                //Check Password Edit Text
                eyes.CheckElement(By.Id("password"));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(String.Format("Error find element: {0}", ex.Message));
            }
            try
            {
                //Check Log-In Button and Remember Check
                eyes.CheckElement(By.Id("log-in"));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(String.Format("Error find element: {0}", ex.Message));
            }
            //Check the Social Login Buttons
            try
            {
                //Check Twitter Button
                eyes.CheckElement(By.CssSelector("img[src='img/social-icons/twitter.png']"), "twitterBt");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(String.Format("Error find element: {0}", ex.Message));
            }
            try
            {
                //Check Facebook Button
                eyes.CheckElement(By.CssSelector("img[src='img/social-icons/facebook.png']"), "facebookBt");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(String.Format("Error find element: {0}", ex.Message));
            }
            try
            {
                //Check Linkedin Button
                eyes.CheckElement(By.CssSelector("img[src='img/social-icons/linkedin.png']"), "linkedinBt");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(String.Format("Error find element: {0}", ex.Message));
            }
            //Close Login Visual Elements Page
            eyes.CloseAsync();
        }


        /// <summary>
        /// Check the Login Dates Cases.
        /// Login without Username and Password
        /// </summary>
        [Test]
        public void Test02LoginDatesErrors()
        {
            eyes.Open(driver, "Hackathon test", "LoginDates Test Hackathon", new Size(800, 600));
            driver.Manage().Window.Size = new Size(800, 600);
            driver.Url = URL;
            //Test login without Username and Password
            try
            {
                driver.FindElement(By.Id("username")).Clear();
                driver.FindElement(By.Id("password")).Clear();
                driver.FindElement(By.Id("log-in")).Click();
                eyes.CheckWindow("Login Without Username and Password");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(String.Format("Error find Element: {0}", ex.Message));
            }
            //Test login without Password
            try
            {
                driver.FindElement(By.Id("username")).SendKeys("Test");
                driver.FindElement(By.Id("password")).Clear();
                driver.FindElement(By.Id("log-in")).Click();
                eyes.CheckWindow("Login Without Password");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(String.Format("Error find Element: {0}", ex.Message));
            }
            //Test login without Username
            try
            {
                driver.FindElement(By.Id("username")).Clear();
                driver.FindElement(By.Id("password")).SendKeys("Test01");
                driver.FindElement(By.Id("log-in")).Click();
                eyes.CheckWindow("Login Without Username");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(String.Format("Error find Element: {0}", ex.Message));
            }
            //Test login with dates
            try
            {
                driver.FindElement(By.Id("username")).Clear();
                driver.FindElement(By.Id("password")).Clear();
                driver.FindElement(By.Id("username")).SendKeys("Test");
                driver.FindElement(By.Id("password")).SendKeys("Test01");
                driver.FindElement(By.Id("log-in")).Click();
                eyes.CheckWindow("Correct login");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(String.Format("Error find Element: {0}", ex.Message));
            }
            eyes.CloseAsync();
        }

        /// <summary>
        /// Login with dates
        /// </summary>
        [Test]
        public void Test03AmountsOrder()
        {
            eyes.Open(driver, "Hackathon test", "Order Column", new Size(800, 600));
            eyes.ForceFullPageScreenshot = true;
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
                eyes.CheckWindow("Order Table Columns");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(String.Format("Error find Element: {0}", ex.Message));
            }
            eyes.CloseAsync();
        }

        /// <summary>
        /// Check the order of chart
        /// </summary>
        [Test]
        public void Test04CanvasChart()
        {
            eyes.Open(driver, "Hackathon test", "Order Canvas Chart", new Size(800, 600));
            eyes.ForceFullPageScreenshot = true;
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
                eyes.CheckWindow("Canvas Chart");
                //Click on Show data for next year
                driver.FindElement(By.Id("addDataset")).Click();
                eyes.CheckWindow("Canvas Chart Next year");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(String.Format("Error find Element: {0}", ex.Message));
            }
            eyes.CloseAsync();
        }


        /// <summary>
        /// Check the gifs on the page
        /// </summary>
        [Test]
        public void Test05DynamicGif()
        {
            eyes.Open(driver, "Hackathon test", "Dynamic Content Gif Test", new Size(800, 600));
            eyes.MatchLevel = MatchLevel.Content;
            driver.Manage().Window.Size = new Size(800, 600);
            driver.Url = URL + "?showAd=true";
            try
            {
                //First Login
                driver.FindElement(By.Id("username")).SendKeys("Test");
                driver.FindElement(By.Id("password")).SendKeys("Test01");
                driver.FindElement(By.Id("log-in")).Click();
                //Check Window (On Applitools we define the Match Region for ignore colors of gif)
                eyes.CheckWindow("Dynamic Content Gif");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(String.Format("Error find Element: {0}", ex.Message));
            }
            eyes.CloseAsync();
        }

        [TearDown]
        public void AfterEach()
        {
            // Close the browser.
            driver.Quit();

            // If the test was aborted before eyes.close was called, ends the test as aborted.
            eyes.AbortIfNotClosed();

            //Wait and collect all test results
            TestResultsSummary allTestResults = runner.GetAllTestResults();
        }
    }
}
