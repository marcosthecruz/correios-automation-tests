
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
    public void AfterScenario()
    {
        if (_container.IsRegistered<IWebDriver>())
        {
            var driver = _container.Resolve<IWebDriver>();

            driver.Quit();
            driver.Dispose();
        }
    }
}
