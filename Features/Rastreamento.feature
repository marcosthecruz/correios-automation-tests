
# language: pt

Funcionalidade: Rastreamento de objetos nos Correios

  @rastreamento
  Cenário: Validar rastreamento com código não localizado
    Quando acessar a página de rastreamento
    E informar o código de rastreamento "SS987654321BR"
    E consultar o código de rastreamento
    Então devo visualizar que o código não foi localizado
