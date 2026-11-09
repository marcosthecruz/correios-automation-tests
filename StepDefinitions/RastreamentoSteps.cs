
using NUnit.Framework;
using OpenQA.Selenium;
using Reqnroll;
using Busca.Cep.Tests.Pages;

namespace Busca.Cep.Tests.StepDefinitions;

[Binding]
public class RastreamentoSteps
{
    private readonly RastreamentoPage _rastreamentoPage;

    public RastreamentoSteps(IWebDriver driver)
    {
        _rastreamentoPage = new RastreamentoPage(driver);
    }

    [When("acessar a página de rastreamento")]
    public void QuandoAcessarAPaginaDeRastreamento()
    {
        _rastreamentoPage.Acessar();
    }

    [When("informar o código de rastreamento {string}")]
    public void QuandoInformarOCodigo(string codigo)
    {
        _rastreamentoPage.PreencherCodigo(codigo);
    }

    [When("consultar o código de rastreamento")]
    public void QuandoConsultarOCodigo()
    {
        _rastreamentoPage.Consultar();
    }

    [Then("devo visualizar que o código não foi localizado")]
    public void EntaoDevoVisualizarCodigoNaoLocalizado()
    {
        Assert.That(
            _rastreamentoPage.CodigoNaoLocalizado(),
            Is.True,
            "A mensagem de código não localizado não foi exibida."
        );
    }
}
