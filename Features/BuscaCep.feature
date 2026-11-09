
# language: pt

Funcionalidade: Busca de CEP nos Correios
  Como usuário dos Correios
  Quero consultar um CEP
  Para verificar se o endereço existe

  @buscaCep
  Cenário: Validar consulta de CEP inexistente e existente
    Dado que estou na página de busca CEP dos Correios
    Então devo visualizar a página de consulta de CEP

    Quando preencher o CEP "80700000"
    E resolver o CAPTCHA manualmente
    Então devo visualizar a mensagem de CEP não encontrado

    Quando retornar para uma nova busca
    Então devo visualizar a página de consulta de CEP

    Quando preencher o CEP "01013-001"
    E resolver o CAPTCHA manualmente
    Então devo visualizar o endereço esperado

    Quando retornar para uma nova busca
    Então devo visualizar a página de consulta de CEP



