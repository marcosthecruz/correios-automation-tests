
using NUnit.Framework;
using OpenQA.Selenium;
using Reqnroll;
using Busca.Cep.Tests.Pages;
using Busca.Cep.Tests.Support;
using OpenQA.Selenium.Support.UI;


namespace Busca.Cep.Tests.StepDefinitions;

[Binding]
public class BuscaCepSteps
{

    private readonly IWebDriver _driver;
    private readonly BuscaCepPage _buscaCepPage;

    public BuscaCepSteps(IWebDriver driver)
    {
        _driver = driver;
        _buscaCepPage = new BuscaCepPage(driver);
    }


    [Given("que estou na página de busca CEP dos Correios")]
    public void DadoQueEstouNaPaginaDeBuscaCep()
    {
        _buscaCepPage.Acessar();
    }


    [Then("devo visualizar a página de consulta de CEP")]
    public void EntaoDevoVisualizarAPagina()
    {
        Assert.That(
            _buscaCepPage.PaginaCarregada(),
            Is.True,
            "A página de busca CEP não foi carregada."
        );
    }


    [When("preencher o CEP {string}")]
    public void QuandoPreencherOCep(string cep)
    {
        _buscaCepPage.PreencherCep(cep);
    }

    [When("clicar no botão Buscar")]
    public void QuandoClicarNoBotaoBuscar()
    {
        _buscaCepPage.BuscarCep();
    }


    [When("resolver o CAPTCHA manualmente")]
    public void QuandoResolverOCaptchaManualmente()
    {
        _buscaCepPage.AguardarCaptchaManual();
    }


    [When("retornar para uma nova busca")]
    public void QuandoRetornarParaUmaNovaBusca()
    {
        _buscaCepPage.NovaBusca();
    }



    [Then("devo visualizar a mensagem de CEP não encontrado")]
    public void EntaoDevoVisualizarMensagemCepNaoEncontrado()
    {
        Assert.That(
            _buscaCepPage.CepNaoEncontrado(),
            Is.True,
            "A mensagem de CEP não encontrado não foi exibida."
        );

        EvidenceHelper.CapturarScreenshot(
            _driver,
            "CEP 80700000 - Nao encontrado"
        );
    }



    [Then("devo visualizar o endereço esperado")]
    public void EntaoDevoVisualizarOEnderecoEsperado()
    {
        Assert.Multiple(() =>
        {
            Assert.That(
                _buscaCepPage.ValidarLogradouro("Rua Quinze de Novembro"),
                Is.True,
                "O logradouro esperado não foi encontrado."
            );

            Assert.That(
                _buscaCepPage.ValidarLocalidade("São Paulo/SP"),
                Is.True,
                "A localidade esperada não foi encontrada."
            );

            EvidenceHelper.CapturarScreenshot(
                _driver,
                "CEP 01013-001 - Endereco encontrado"
            );
        });

    }


}
