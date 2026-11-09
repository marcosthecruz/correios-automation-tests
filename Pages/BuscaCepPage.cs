
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Busca.Cep.Tests.Pages;

public class BuscaCepPage
{
    private readonly IWebDriver _driver;

    private const string Url =
        "https://buscacepinter.correios.com.br/app/endereco/index.php";

    //Seletores
    private readonly By campoCep = By.Id("endereco");
    private readonly By botaoBuscar = By.Id("btn_pesquisar");
    private readonly By mensagemCepNaoEncontrado =
        By.XPath("//div[@id='mensagem-resultado-alerta']/h6");
    private readonly By botaoNovaBusca = By.Id("btn_nbusca");
    private readonly By resultadoLogradouro =
        By.CssSelector("td[data-th='Logradouro/Nome']");
    private readonly By resultadoLocalidade =
        By.CssSelector("td[data-th='Localidade/UF']");





    public BuscaCepPage(IWebDriver driver)
    {
        _driver = driver;
    }

    public void Acessar()
    {
        _driver.Navigate().GoToUrl(Url);
    }

    public bool PaginaCarregada()
    {
        var wait = new WebDriverWait(
            _driver, TimeSpan.FromSeconds(15));

        return wait.Until(driver =>
            driver.FindElements(
                By.XPath("//h2[contains(.,'Busca CEP')]")
            ).Any(element => element.Displayed));
    }

    public void PreencherCep(string cep)
    {
        var wait = new WebDriverWait(
            _driver, TimeSpan.FromSeconds(10));

        var campo = wait.Until(driver =>
        {
            var elemento = driver.FindElement(campoCep);

            return elemento.Displayed && elemento.Enabled
                ? elemento
                : null;
        });

        campo.Clear();
        campo.SendKeys(cep);
    }


    public void BuscarCep()
    {
        var wait = new WebDriverWait(
            _driver, TimeSpan.FromSeconds(10));

        var botao = wait.Until(driver =>
        {
            var elemento = driver.FindElement(botaoBuscar);

            return elemento.Displayed && elemento.Enabled
                ? elemento
                : null;
        });

        botao.Click();
        Thread.Sleep(5000);
    }


    public bool CepNaoEncontrado()
    {
        var wait = new WebDriverWait(
            _driver, TimeSpan.FromSeconds(15));

        return wait.Until(driver =>
        {
            var mensagens = driver.FindElements(
                mensagemCepNaoEncontrado);

            return mensagens.Any(elemento =>
                elemento.Displayed &&
                elemento.Text.Trim() == "Dados não encontrado");
        });
    }


    public void NovaBusca()
    {
        var wait = new WebDriverWait(
            _driver, TimeSpan.FromSeconds(10));

        // Aguarda o botão Nova Busca estar disponível
        var botao = wait.Until(driver =>
        {
            var elemento = driver.FindElement(botaoNovaBusca);

            return elemento.Displayed && elemento.Enabled
                ? elemento
                : null;
        });

        // Retorna ao formulário de consulta
        botao.Click();

        // Confirma que o campo de CEP está disponível novamente
        wait.Until(driver =>
        {
            var campos = driver.FindElements(campoCep);

            return campos.Any(elemento =>
                elemento.Displayed && elemento.Enabled);
        });
    }


    public bool ValidarLogradouro(string logradouroEsperado)
    {
        var wait = new WebDriverWait(
            _driver, TimeSpan.FromSeconds(15));

        return wait.Until(driver =>
        {
            var elementos = driver.FindElements(resultadoLogradouro);

            return elementos.Any(elemento =>
                elemento.Displayed &&
                elemento.Text.Contains(
                    logradouroEsperado,
                    StringComparison.OrdinalIgnoreCase));
        });
    }


    public bool ValidarLocalidade(string localidadeEsperada)
    {
        var wait = new WebDriverWait(
            _driver, TimeSpan.FromSeconds(15));

        return wait.Until(driver =>
        {
            var elementos = driver.FindElements(resultadoLocalidade);

            return elementos.Any(elemento =>
                elemento.Displayed &&
                elemento.Text.Trim().Equals(
                    localidadeEsperada,
                    StringComparison.OrdinalIgnoreCase));
        });
    }


    public void AguardarResultadoConsulta()
    {
        var wait = new WebDriverWait(
            _driver, TimeSpan.FromMinutes(2));

        wait.Until(driver =>
        {
            // Aguarda o usuário resolver o CAPTCHA
            // e a página apresentar o resultado.
            return driver.Url.Contains("resultado");
        });
    }



}
