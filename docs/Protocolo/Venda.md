***Introdução***
==========

Este documento descreve a comunicação entre o servidor e a vending machine durante uma **transação de venda**.


***Venda***
=====

Uma transação de venda é finalizada após os passos:

1. Usuário escolheu seu(s) produto(s);
2. Usuário apresentou cartão;
3. Vending machine enviou requisição para o servidor com informações de código de cliente e relação de produto(s);
4. Servidor verifica ID e saldo disponível, respondendo com código de transação e aprovação;
5. Caso a venda tenha sido aprovada, a vending machine concede* os produtos. 

_(*) caso o equipamento apresente defeito, uma requisição de estorno será enfileirada e enviada assim que possível_

A comunicação entre cliente e servidor acontece nos passos 3 e 4.

***Requisição de venda da vending machine***
=

A requisição de venda da vending machine possui a seguinte estrutura, onde a unidade das colunas offset e tamanho é byte:

| Offset | Tamanho | Campo          | Tipo | Descrição                                                    |
|:-------|:--------|:---------------|------|--------------------------------------------------------------|
| 0 | 2 | VendingMachineID | Int16 | O código de identificação da VendingMachine que iniciou a venda. |
| 2 | 7 | ClientCardID | byte[7] | O código de identificação do cartão usado para pagamento da compra. |
| 9 | 1 | ItemsCount | byte | A quantidade de itens vendidos. |
| 10 | (n * 1) | Items | byte[n] | O código de cada produto da venda. |
| 10 + (n * 1) | 4 | Price | Float | O valor da compra calculado pela vending machine e apresentada ao usuário. |


Exemplo
=

A requisição com a cadeia de bytes abaixo:

    0x00 0x24 0xFF 0xFF 0xFF 0xFF 0xFF 0xFF 0xFF 0x02 0x12 0x13 0x00 0x00 0x20 0x40
    
Traz os valores:
- **VendingMachineID:** 24;
- **ClientCardID:** FF FF FF FF FF FF FF;
- **ItemsCount:** 2;
- **Items:** 12 e 13;
- **Price:** 2,50.

***Resposta do servidor***
============

A resposta do servidor possui a seguinte estrutura, onde a unidade das colunas offset e tamanho é byte:

| Offset | Tamanho | Campo          | Tipo | Descrição                                                    |
|:-------|:--------|:---------------|------|--------------------------------------------------------------|
| 0 | 2 | TransactionId | Int16 | O código de identificação da transação. |
| 2 | 1 | TransactionResult | byte (bit field) | Status de resultado da transação. |
| 3 | 4 | CreditAfterTransaction | float | Créditos após transação - seja ela de sucesso ou não. |

**TransactionResult:**

O campo *TransactionResult* possui as máscaras:

| Máscara | Tipo | Descrição |
|:------|:-----|:--------- |
| 0b10000000 | Sucesso | A transação foi realizada com sucesso e os créditos do usuário foram descontados. |
| 0b01000000 | Falha | Saldo insuficiente. |
| 0b00100000 | Falha | Valor calculado pela VendingMachine e servidor não correspondem. |
| 0b00010000 | Falha | Usuário não cadastrado. |
| 0b00001000 | Falha | Um ou mais códigos de produto incorretos. |

Exemplo
=

A requisição com a cadeia de bytes abaixo:

    0x00 0x02 0x80 0x00 0x00 0xF0 0x42
    
Traz os valores:
- **TransactionId:** 2;
- **TransactionResult:** Sucesso;
- **CreditAfterTransaction:** 120,00;
