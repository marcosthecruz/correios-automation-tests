
using NUnit.Framework;
using OpenQA.Selenium;
using Reqnroll;
using Busca.Cep.Tests.Pages;
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
        Console.WriteLine(
            "Preencha o CAPTCHA e clique em Buscar no navegador.");

        var wait = new WebDriverWait(
            _driver, TimeSpan.FromMinutes(2));

        wait.PollingInterval = TimeSpan.FromMilliseconds(500);

        wait.Until(driver =>
        {
            // Resultado de CEP inexistente
            bool cepNaoEncontrado = driver.FindElements(
                By.Id("mensagem-resultado-alerta"))
                .Any(elemento =>
                    elemento.Displayed &&
                    elemento.Text.Contains("Dados não encontrado"));

            // Resultado de CEP existente
            bool cepEncontrado = driver.FindElements(
                By.CssSelector("td[data-th='Logradouro/Nome']"))
                .Any(elemento =>
                    elemento.Displayed &&
                    !string.IsNullOrWhiteSpace(elemento.Text));

            return cepNaoEncontrado || cepEncontrado;
        });

        Console.WriteLine("Resultado da consulta identificado.");
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

        Thread.Sleep(10000); 
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
        });

        Thread.Sleep(10000); 
    }





}
