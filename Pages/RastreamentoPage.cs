
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Busca.Cep.Tests.Pages;

public class RastreamentoPage
{
    private readonly IWebDriver _driver;

    private const string Url =
        "https://rastreamento.correios.com.br/app/index.php";

    // Seletores
    private readonly By campoRastreamento =
        By.Id("objeto");

    private readonly By botaoConsultar =
        By.Id("b-pesquisar");

    private readonly By mensagemResultado =
        By.CssSelector("div.msg");

    public RastreamentoPage(IWebDriver driver)
    {
        _driver = driver;
    }

    public void Acessar()
    {
        _driver.Navigate().GoToUrl(Url);
    }

    public void PreencherCodigo(string codigo)
    {
        var wait = new WebDriverWait(
            _driver, TimeSpan.FromSeconds(10));

        var campo = wait.Until(driver =>
        {
            var elemento = driver.FindElement(
                campoRastreamento);

            return elemento.Displayed && elemento.Enabled
                ? elemento
                : null;
        });

        campo.Clear();
        campo.SendKeys(codigo);
    }

    public void Consultar()
    {
        var wait = new WebDriverWait(
            _driver, TimeSpan.FromSeconds(10));

        var botao = wait.Until(driver =>
        {
            var elemento = driver.FindElement(
                botaoConsultar);

            return elemento.Displayed && elemento.Enabled
                ? elemento
                : null;
        });

        botao.Click();
    }


    public bool CodigoNaoLocalizado()
    {
        Console.WriteLine(
            "Aguardando resultado do rastreamento.");

        Console.WriteLine(
            "Se aparecer a verificação de segurança, " +
            "conclua manualmente no navegador.");

        Console.WriteLine(
            "Se necessário, clique novamente em Consultar.");

        var wait = new WebDriverWait(
            _driver, TimeSpan.FromMinutes(2));

        wait.PollingInterval = TimeSpan.FromMilliseconds(500);

        wait.Message =
            "O resultado do rastreamento não apareceu. " +
            "Verifique se a validação de segurança foi concluída.";

        return wait.Until(driver =>
        {
            var mensagens = driver.FindElements(
                By.CssSelector("div.msg"));

            return mensagens.Any(elemento =>
                elemento.Displayed &&
                elemento.Text.Contains(
                    "Não localizamos informações para o código informado",
                    StringComparison.OrdinalIgnoreCase));
        });
    }

}
