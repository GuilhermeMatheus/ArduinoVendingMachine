***Introdução***
==========

Este documento descreve a comunicação entre o servidor e a vending machine durante a **inicialização da máquina**.


***Inicialização***
=====

Sempre que uma vending machine é inicializada, ela deve se comunicar com o servidor indicando este evento e, através desta conexão inicial, o servidor será capaz de recuperar informações de rede entre os dois.

***Requisição de venda da vending machine***
=

A requisição de inicialização da vending machine possui a seguinte estrutura, onde a unidade das colunas offset e tamanho é byte:

| Offset     | Tamanho | Campo          | Tipo | Descrição                                                    |
|:-----------|:--------|:---------------|------|--------------------------------------------------------------|
| 0 | 1 | Início de requisição | Byte | A intenção de pedido de venda. Veja mais [aqui](README.md#venda). |
| 1 | 2 | VendingMachineID | Int16 | O código de identificação da VendingMachine em inicialização. |

Exemplo
=

A requisição com a cadeia de bytes abaixo:

    0x03 0x01

Traz os valores:
- **Intenção:** InicializacaoMaquina;
- **VendingMachineID:** 1;

Além dos dados fornecidos pela estrutura usada, é possível obtermos informações importantes como o endereço IP da VendingMachine por meio das outras camadas de rede.

***Resposta do servidor***
============

A resposta do servidor possui a seguinte estrutura, onde a unidade das colunas offset e tamanho é byte:

| Offset | Tamanho | Campo          | Tipo | Descrição                                                    |
|:-------|:--------|:---------------|------|--------------------------------------------------------------|
| 0 | 1 | StartEventResult | Int8 | O resultado do processamento do evento. |

**StartEventResult:**

O campo *StartEventResult* possui as máscaras:

| Máscara | Tipo | Descrição |
|:------|:-----|:--------- |
| 0b10000000 | Sucesso | O evento de inicialização foi processado com sucesso. |
| 0b01000000 | Falha | Máquina não reconhecida. |

Exemplo
=

A resposta com a cadeia de bytes abaixo:

    0x01
    
Traz o valor:
- **StartEventResult:** Sucesso;
