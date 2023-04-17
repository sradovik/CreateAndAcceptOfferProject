using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using Expect = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace homeworkFinal
{
    [TestFixture]
    public class final
    {
        public IWebDriver Driver;
        public WebDriverWait wait;
        public static Random random = new Random();

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
        }

        [SetUp]
        public void SetUp()
        {
            Driver = new ChromeDriver();

            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);
            Driver.Manage().Window.Maximize();

            Driver.Navigate().GoToUrl("http://18.156.17.83:9095/");

            wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(30));
        }


        [Test]
        public void createAndAcceptTheOffer()
        {

            IWebElement signInButton = Driver.FindElement(By.Id("login"));
            signInButton.Click();

            IWebElement usernameField = Driver.FindElement(By.Id("username"));
            usernameField.Clear();
            usernameField.SendKeys("");

            IWebElement passwordField = Driver.FindElement(By.Id("password"));
            passwordField.Clear();
            passwordField.SendKeys("");

            IWebElement loggInButton = Driver.FindElement(By.CssSelector("button[translate='login.form.button']"));
            loggInButton.Click();

            string Url = "http://18.156.17.83:9095/client/home";
            wait.Until(Expect.UrlToBe(Url));

            IWebElement createRequestButton = Driver.FindElement(By.CssSelector("span[translate='provider.createRequest']"));
            createRequestButton.Click();

            IWebElement titleField = Driver.FindElement(By.CssSelector("input[ng-model='vm.request.title']"));
            titleField.Clear();
            titleField.SendKeys("Chair");

            IWebElement categoryField = Driver.FindElement(By.Id("field_y"));
            SelectElement requestCategory = new SelectElement(categoryField);
            requestCategory.SelectByIndex(5);

            IWebElement pickUpField = Driver.FindElement(By.Name("pickUpAddress"));
            IWebElement adress = pickUpField.FindElement(By.CssSelector("input[ng-value='vm.address.formattedAddress']"));
            adress.SendKeys("Skopje, North Macedonia");
            List<IWebElement> autoPickUpAddress = Driver.FindElements(By.CssSelector("span[class='pac-matched']")).ToList();
            autoPickUpAddress[0].Click();

            IWebElement deliveryField = Driver.FindElement(By.Name("deliveryAddress"));
            IWebElement deliveryAdress = deliveryField.FindElement(By.CssSelector("input[ng-value='vm.address.formattedAddress']"));
            deliveryAdress.SendKeys("Skopje, North Macedonia");
            List<IWebElement> autoDeliveryAddress = Driver.FindElements(By.CssSelector("span[class='pac-matched']")).ToList();
            autoDeliveryAddress[0].Click();

            IWebElement setPickUpDateCheckBox = Driver.FindElement(By.Id("setPickUpDate"));
            setPickUpDateCheckBox.Click();

            IWebElement earliestPickUpDateField = Driver.FindElement(By.CssSelector("input[ng-model='vm.request.earliestPickUpDate']"));
            earliestPickUpDateField.Clear();
            earliestPickUpDateField.SendKeys("18.03.2023");

            IWebElement latestPickUpDate = Driver.FindElement(By.CssSelector("input[ng-model='vm.request.latestPickUpDate']"));
            latestPickUpDate.Clear();
            latestPickUpDate.SendKeys("20.03.2023");

            IWebElement descriptionArea = Driver.FindElement(By.CssSelector("textarea[ng-model='vm.request.description']"));
            descriptionArea.Clear();
            descriptionArea.SendKeys(RandomGenerateLetters(10));

            IWebElement removeItemButton = Driver.FindElement(By.CssSelector("a[ng-click='vm.removeDimension(dimension);']"));
            removeItemButton.Click();


            IWebElement cashOnPickUpBox = Driver.FindElement(By.Id("cachePickup"));
            cashOnPickUpBox.Click();

            IWebElement cashOnDeliveryBox = Driver.FindElement(By.Id("cacheDelivery"));
            cashOnDeliveryBox.Click();

            IWebElement inAdvanceBox = Driver.FindElement(By.Id("advance"));
            inAdvanceBox.Click();

            IWebElement submitRequestButton = Driver.FindElement(By.CssSelector("input[class='btn btn-green center-block']"));
            submitRequestButton.Click();

            string expectedURL = "http://18.156.17.83:9095/client/my-requests/active";

            wait.Until(Expect.UrlContains(expectedURL));
            Assert.AreEqual(expectedURL, Driver.Url);

            IWebElement activeRequests = Driver.FindElement(By.CssSelector("a[ui-sref='client-my-active-requests']"));
            Assert.IsTrue(activeRequests.Displayed);


            IWebElement tableBody = Driver.FindElement(By.ClassName("table-body"));

            List<IWebElement> listOfRows = tableBody.FindElements(By.TagName("tr")).ToList();

            IWebElement firstRow = listOfRows.First();

            wait.Until(Expect.ElementIsVisible(By.CssSelector("td[class='table-body__cell column1']")));
            IWebElement firstColumn = firstRow.FindElement(By.CssSelector("td[class='table-body__cell column1']"));
            string text = firstColumn.Text;
            Assert.AreEqual("Chair", text);

            wait.Until(Expect.ElementIsVisible(By.CssSelector("a[ng-click='vm.logout()']")));
            IWebElement logOutbutton = Driver.FindElement(By.CssSelector("a[ng-click='vm.logout()']"));
            logOutbutton.Click();

            string ExpectedHomepageUrl = "http://18.156.17.83:9095/";
            wait.Until(Expect.UrlToBe(ExpectedHomepageUrl));



            IWebElement signInButton2 = Driver.FindElement(By.Id("login"));
            signInButton2.Click();

            IWebElement usernameField2 = Driver.FindElement(By.Id("username"));
            usernameField2.Clear();
            usernameField2.SendKeys("");

            IWebElement passwordField2 = Driver.FindElement(By.Id("password"));
            passwordField2.Clear();
            passwordField2.SendKeys("");

            IWebElement logInButton = Driver.FindElement(By.CssSelector("button[translate='login.form.button']"));
            logInButton.Click();

            IWebElement titleText = Driver.FindElement(By.CssSelector("a[ui-sref='provider-request-details({id: request.id})']"));
            titleText.Click();

            IWebElement createOfferButton = Driver.FindElement(By.CssSelector("button[ui-sref='make-offer({request: vm.request})']"));
            createOfferButton.Click();

            IWebElement priceField = Driver.FindElement(By.CssSelector("input[ng-model='paymentType.price']"));
            priceField.Clear();
            priceField.SendKeys("0.1");

            IWebElement validOfferField = Driver.FindElement(By.CssSelector("input[ng-model='vm.expirationDate']"));
            validOfferField.Clear();
            validOfferField.SendKeys("20.03.2023 20:00");

            IWebElement messageToClienArea = Driver.FindElement(By.CssSelector("textarea[ng-model='vm.offerComment']"));
            messageToClienArea.Clear();
            messageToClienArea.SendKeys(RandomGenerateLetters(15));

            IWebElement createAnOfferButton = Driver.FindElement(By.ClassName("make-offer__btn-create"));
            createAnOfferButton.Click();

            IWebElement confirmTheOfferButton = Driver.FindElement(By.ClassName("modal-footer__btn-save"));
            confirmTheOfferButton.Click();

            Driver.Navigate().Refresh();            

             IWebElement myOffersButton = Driver.FindElement(By.CssSelector("a[ui-sref='provider-my-active-offers']"));
            myOffersButton.Click();

            IWebElement activeOffersTableBody = Driver.FindElement(By.ClassName("table-body"));

            List<IWebElement> listOfRow1 = activeOffersTableBody.FindElements(By.TagName("tr")).ToList();

            IWebElement rowOne = listOfRow1.First();

            IWebElement columnOne = rowOne.FindElement(By.CssSelector("td[class='table-body__cell column1']"));
            StringAssert.Contains("Chair", columnOne.Text);

            wait.Until(Expect.ElementIsVisible(By.CssSelector("a[ng-click='vm.logout()']")));
            IWebElement loggoutButton = Driver.FindElement(By.CssSelector("a[ng-click='vm.logout()']"));
            loggoutButton.Click();


            string ExpectedUrl = "http://18.156.17.83:9095/";
            wait.Until(Expect.UrlToBe(ExpectedUrl));


            IWebElement signInButton3 = Driver.FindElement(By.Id("login"));
            signInButton3.Click();

            IWebElement usernameField3 = Driver.FindElement(By.Id("username"));
            usernameField3.Clear();
            usernameField3.SendKeys("");

            IWebElement passwordField3 = Driver.FindElement(By.Id("password"));
            passwordField3.Clear();
            passwordField3.SendKeys("");

            wait.Until(Expect.ElementIsVisible(By.CssSelector("button[translate='login.form.button']")));
            IWebElement loginButton = Driver.FindElement(By.CssSelector("button[translate='login.form.button']"));
            loginButton.Click();

            string expectedUrl = "http://18.156.17.83:9095/client/home";
            wait.Until(Expect.UrlToBe(expectedUrl));

            IWebElement myRequestsButton = Driver.FindElement(By.CssSelector("a[ui-sref='client-my-requests']"));
            myRequestsButton.Click();

            IWebElement tableBodyUser = Driver.FindElement(By.ClassName("table-body"));
            List<IWebElement> listOfRow = tableBodyUser.FindElements(By.TagName("tr")).ToList();

            IWebElement titleText2 = Driver.FindElement(By.CssSelector("a[ui-sref='client-request-details({id: request.id})']"));
            titleText2.Click();

            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");

            wait.Until(Expect.ElementIsVisible(By.CssSelector("div[class='flex-table__body']")));
            IWebElement offersTable = Driver.FindElement(By.CssSelector("div[class='flex-table__body']"));
            List<IWebElement> offersList = offersTable.FindElements(By.TagName("div")).ToList();

            IWebElement rows = offersList[8];

            IWebElement moreButton = Driver.FindElement(By.CssSelector("a[translate='request.more']"));
            moreButton.Click();


            IWebElement offersExpanded = Driver.FindElement(By.CssSelector("div[class='row offers-set-details__row']"));
            IWebElement offerButton = Driver.FindElement(By.Id("offer0"));
            offerButton.Click();

            IWebElement acceptButton = Driver.FindElement(By.CssSelector("input[ng-click='vm.acceptOffer(offersSet);']"));
            acceptButton.Click();

            IWebElement thetableBody = Driver.FindElement(By.ClassName("table-body"));
            List<IWebElement> rowsListed = thetableBody.FindElements(By.TagName("tr")).ToList();
            
            IWebElement rowFirst = rowsListed[0];

            IWebElement column1 = rowFirst.FindElement(By.CssSelector("td[class='table-body__cell column1']"));
            Assert.IsTrue(column1.Displayed);



            IWebElement myReqButton = Driver.FindElement(By.CssSelector("a[ui-sref='client-my-requests']"));
            myReqButton.Click();

            wait.Until(Expect.ElementIsVisible(By.CssSelector("div[translate='request.noRequests']")));
            IWebElement emptyRequestMessage = Driver.FindElement(By.CssSelector("div[translate='request.noRequests']"));
            Assert.True(emptyRequestMessage.Displayed, $"Sorry! There are no requests in this list.");

            wait.Until(Expect.ElementIsVisible(By.CssSelector("a[ng-click='vm.logout()']")));
            IWebElement signOutButton = Driver.FindElement(By.CssSelector("a[ng-click='vm.logout()']"));
            signOutButton.Click();


            string HomeUrl = "http://18.156.17.83:9095/";
            wait.Until(Expect.UrlToBe(HomeUrl));


        }








        [TearDown]
        public void Teardown()
        {
           Driver.Close();
           Driver.Dispose();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {

        }


        public static string RandomGenerateLetters(int lenght)
        {
            const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrsqtuvwxyz";
            return new string(Enumerable.Repeat(letters, lenght)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }



    }
}





