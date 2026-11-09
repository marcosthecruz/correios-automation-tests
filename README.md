# Automação de Testes — Correios | Avaliação Técnica B3

Projeto desenvolvido para uma avaliação técnica de automação de testes, utilizando C#, Selenium WebDriver, Reqnroll (BDD) e NUnit.

## Objetivo

Automatizar os principais fluxos de consulta de CEP e rastreamento de objetos no site dos Correios, validando os resultados apresentados ao usuário.

## Tecnologias utilizadas

- C# / .NET 10
- Selenium WebDriver
- Reqnroll — BDD com Gherkin
- NUnit — Framework de testes
- Page Object Model (POM)
- Git e GitHub
- Visual Studio Code

## Estrutura do projeto

```text
Busca.Cep.Tests/
├── Features/
│   ├── BuscaCep.feature
│   └── Rastreamento.feature
├── StepDefinitions/
│   ├── BuscaCepSteps.cs
│   └── RastreamentoSteps.cs
├── Pages/
│   ├── BuscaCepPage.cs
│   ├── HomePage.cs
│   └── RastreamentoPage.cs
├── Drivers/
│   └── WebDriverFactory.cs
├── Hooks/
│   └── TestHooks.cs
├── Support/
├── Evidencias/
├── Busca.Cep.Tests.csproj
├── .gitignore
└── README.md
```

## Cenários implementados

**1. Consulta de CEP inexistente**

- Acessar a página dos Correios.
- Consultar o CEP `80700000`.
- Validar a mensagem "Dados não encontrado".
- Retornar à tela inicial.

**2. Consulta de CEP existente**

- Consultar o CEP `01013-001`.
- Validar o logradouro "Rua Quinze de Novembro".
- Validar a localidade "São Paulo/SP".
- Retornar à tela inicial.

**3. Rastreamento de objeto**

- Acessar a página de rastreamento.
- Informar o código `SS987654321BR`.
- Consultar o código.
- Validar a mensagem de código não localizado.

**Status:** a implementação do rastreamento está sujeita a bloqueio pela verificação de segurança do Cloudflare no ambiente público dos Correios. A validação automatizada desse cenário não foi concluída com sucesso.

## Estratégia de automação

O projeto utiliza Page Object Model para separar os elementos e as interações com as páginas dos cenários BDD.

Foram utilizados três tipos de seletores solicitados na avaliação:

- ID — identificação de campos e botões.
- XPath — validação de mensagens.
- CSS Selector — validação de dados retornados nas consultas.

Também foram utilizadas esperas explícitas com WebDriverWait, evitando pausas fixas desnecessárias.

Os testes utilizam Hooks para inicializar e encerrar o navegador automaticamente.

## Como executar

**Pré-requisitos:**

- .NET 10 SDK
- Google Chrome instalado
- Git
- Acesso à internet

**1. Clonar o repositório**

```bash
git clone URL_DO_REPOSITORIO
cd Busca.Cep.Tests
```

**2. Restaurar as dependências**

```bash
dotnet restore
```

**3. Compilar o projeto**

```bash
dotnet build
```

**4. Executar os testes de CEP**

```bash
dotnet test --filter "TestCategory=buscaCep" --logger "console;verbosity=normal"
```

As consultas exigem preenchimento manual de CAPTCHA na página dos Correios.

## Resultados e evidências

O cenário integrado das duas consultas de CEP foi aprovado na execução local, com validação do CEP inexistente, endereço encontrado e retorno à busca.

As evidências disponíveis ficam na pasta `Evidencias/`.

## Limitações conhecidas

**CAPTCHA — Busca CEP**

O site dos Correios exige preenchimento manual do CAPTCHA. O Selenium realiza o preenchimento dos dados, a navegação e as validações, enquanto a etapa de segurança depende de intervenção humana.

**Cloudflare — Rastreamento**

A página de rastreamento apresenta uma verificação de segurança do Cloudflare, que bloqueou a continuidade da automação mesmo após tentativa de confirmação humana.

Por esse motivo, o cenário de rastreamento foi mantido no projeto, mas não é apresentado como validado com sucesso.

**Execução em CI/CD**

Os cenários que dependem de CAPTCHA não são adequados para execução totalmente desassistida no ambiente público. Em um projeto real, o ideal seria utilizar um ambiente de homologação autorizado com mecanismos de teste apropriados para viabilizar a automação contínua.

## Versionamento

O código é versionado utilizando Git, com desenvolvimento em branches e integração por Pull Request.

O projeto está preparado para execução pelo terminal utilizando o .NET CLI.

---

Desenvolvido como parte da avaliação técnica de automação de testes da B3.