
using Busca.Cep.Tests.Support;
using Reqnroll.BoDi;
using OpenQA.Selenium;
using Reqnroll;
using Busca.Cep.Tests.Drivers;

namespace Busca.Cep.Tests.Hooks;

[Binding]
public class TestHooks
{
    private readonly IObjectContainer _container;

    public TestHooks(IObjectContainer container)
    {
        _container = container;
    }

    [BeforeScenario]
    public void BeforeScenario()
    {
        IWebDriver driver = WebDriverFactory.CreateDriver();

        _container.RegisterInstanceAs<IWebDriver>(driver);
    }


    [AfterScenario]
    public void AfterScenario(ScenarioContext scenarioContext)
    {
        if (!_container.IsRegistered<IWebDriver>())
            return;

        var driver = _container.Resolve<IWebDriver>();

        try
        {
            if (scenarioContext.TestError != null)
            {
                EvidenceHelper.CapturarScreenshot(
                    driver,
                    "Falha - " + scenarioContext.ScenarioInfo.Title
                );
            }
        }
        catch (WebDriverException ex)
        {
            Console.WriteLine(
                $"Não foi possível capturar o screenshot: {ex.Message}");
        }
        finally
        {
            driver.Quit();
            driver.Dispose();
        }
    }

}
