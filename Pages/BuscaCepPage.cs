
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;

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



    public void AguardarCaptchaManual()
    {
        TestContext.Progress.WriteLine("");
        TestContext.Progress.WriteLine("========================================");
        TestContext.Progress.WriteLine("       INTERVENÇÃO MANUAL - CAPTCHA");
        TestContext.Progress.WriteLine("========================================");
        TestContext.Progress.WriteLine("1. Acesse a janela do Chrome.");
        TestContext.Progress.WriteLine("2. Digite o CAPTCHA apresentado.");
        TestContext.Progress.WriteLine("3. Pressione TAB ao terminar.");
        TestContext.Progress.WriteLine("4. NÃO clique no botão Buscar.");
        TestContext.Progress.WriteLine("O Selenium continuará automaticamente.");
        TestContext.Progress.WriteLine("Tempo máximo: 120 segundos.");
        TestContext.Progress.WriteLine("========================================");


        ((IJavaScriptExecutor)_driver).ExecuteScript(@"
            if (!document.getElementById('aviso-automacao')) {
                const aviso = document.createElement('div');
                aviso.id = 'aviso-automacao';
                aviso.style.cssText = `
                    position: fixed;
                    top: 10px;
                    right: 10px;
                    background: #fff3cd;
                    color: #333;
                    padding: 15px;
                    border: 2px solid #ffc107;
                    border-radius: 8px;
                    z-index: 99999;
                    font-size: 15px;
                    max-width: 320px;
                `;
                aviso.innerText =
                    'AUTOMAÇÃO DE TESTES\n' +
                    '1. Preencha o CAPTCHA.\n' +
                    '2. Pressione TAB ao terminar.\n' +
                    '3. Não clique em Buscar.\n' +
                    'O Selenium continuará automaticamente.';
                document.body.appendChild(aviso);
            }
        ");


        var wait = new WebDriverWait(
            _driver, TimeSpan.FromMinutes(2));

        wait.Until(driver =>
        {
            var campo = driver.FindElement(By.Id("captcha"));

            bool preenchido = !string.IsNullOrWhiteSpace(
                campo.GetDomProperty("value"));

            bool saiuDoCampo =
                (bool)((IJavaScriptExecutor)driver)
                    .ExecuteScript(
                        "return document.activeElement !== arguments[0];",
                        campo)!;

            return preenchido && saiuDoCampo;
        });

        Console.WriteLine("");
        // Console.WriteLine("CAPTCHA preenchido. Continuando a automação...");
        TestContext.Progress.WriteLine("CAPTCHA preenchido. Continuando a automação...");
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
