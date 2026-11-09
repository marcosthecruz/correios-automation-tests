
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Busca.Cep.Tests.Drivers;

public static class WebDriverFactory
{
    public static IWebDriver CreateDriver()
    {
        var options = new ChromeOptions();

        options.AddArgument("--start-maximized");
        options.AddArgument("--disable-notifications");

        return new ChromeDriver(options);
    }
}
