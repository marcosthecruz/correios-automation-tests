
using Allure.Net.Commons;
using OpenQA.Selenium;

namespace Busca.Cep.Tests.Support;

public static class EvidenceHelper
{
    public static void CapturarScreenshot(
        IWebDriver driver,
        string nome)
    {
        var screenshot =
            ((ITakesScreenshot)driver).GetScreenshot();

        AllureApi.AddAttachment(
            nome + ".png",
            "image/png",
            screenshot.AsByteArray
        );
    }
}
