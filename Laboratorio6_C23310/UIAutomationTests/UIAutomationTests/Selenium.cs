using System.Diagnostics;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V135.Audits;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace UIAutomationTests
{
  public class Selenium
  {
    EdgeDriver driver;
    [SetUp]
    public void Setup()
    {
      driver = new EdgeDriver();
    }

    [TearDown]
    public void TearDown()
    {
      driver.Dispose();
    }

    [Test]
    public void Enter_To_List_Of_Countries_Test()
    {
      var URL = "http://localhost:8080/";
      var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

      driver.Manage().Window.Maximize();
      driver.Navigate().GoToUrl(URL);

      int rowCount = 0;
      try
      {
        var table = wait.Until(driver => driver.FindElement(By.CssSelector(
          "#app > div > table")));
        var rows = table.FindElements(By.TagName("tr"));
        rowCount = rows.Count;
      }
      catch (NoSuchElementException ex)
      {
        Assert.Fail("Table not found: " + ex.Message);
      }

      try
      {
        var addCountryButton = wait.Until(ExpectedConditions
          .ElementToBeClickable(By.CssSelector("#app > div > div > div > a >" +
          " button")));
        addCountryButton.Click();

        var country = "Australia";
        var nameInput = wait.Until(ExpectedConditions.ElementIsVisible(
          By.Id("name")));
        nameInput.SendKeys(country);

        var option = 6; // Oceania
        var continentSelect = wait.Until(driver =>
            driver.FindElement(By.CssSelector("#continente > option:nth-child("
              + option + ")")));
        continentSelect.Click();

        var language = "Inglés";
        var languageSelect = driver.FindElement(By.Id("idioma"));
        languageSelect.SendKeys(language);

        var confirmButton = driver.FindElement(By.CssSelector(
            "#app > div.d-flex.justify-content-center.align-items-center.vh-100"
              + " > div > form > div:nth-child(4) > button"));
        confirmButton.Click();
      }
      catch (Exception ex)
      {
        Assert.Fail("Form failed: " + ex.Message);
      }

      bool messageDisplayed = false;
      string? messageText = "";

      try
      {
        var messageBox = wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("#app > div.d-flex.justify-content-center." +
              "align-items-center.vh-100 > div > div")));

        messageDisplayed = messageBox.Displayed;
        messageText = messageBox.Text;
      }
      catch (WebDriverTimeoutException ex)
      {
        Assert.Fail("Could not add country: " + ex.Message);
      }

      driver.Navigate().GoToUrl(URL);

      int rowCountUpdated = 0;
      try
      {
        var table = wait.Until(driver => driver.FindElement(By.CssSelector(
          "#app > div > table")));
        var rows = table.FindElements(By.TagName("tr"));
        rowCountUpdated = rows.Count;
      }
      catch (NoSuchElementException ex)
      {
        Assert.Fail("Table not found: " + ex.Message);
      }

      if (messageText == null)
      {
        messageText = "";
      }

      Assert.That(messageDisplayed, Is.True);
      Assert.That(messageText.Contains("Pais guardado exitosamente!"), Is.True);
      Assert.Greater(rowCountUpdated, rowCount);
    }
  }
}
