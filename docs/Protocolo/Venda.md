Introdução
==========

Este documento descreve a comunicação entre o servidor e a vending machine durante uma *transação de venda*.

Definições
==========

*Código de cliente:* O ID do usuário é uma cadeia de 8 bytes à partir do identificador do cartão compatível com RFID;
*Código de Transação:* Toda transação, seja concluída com sucesso ou não, possui um código de número inteiro de 32 bits;


Venda
=====

Uma transação de venda é finalizada após os passos:

1. Usuário escolheu seu(s) produto(s);
2. Usuário apresentou cartão;
3. Vending machine enviou requisição para o servidor com informações de código de cliente e relação de produto(s);
4. Servidor verifica ID e saldo disponível, respondendo com código de transação e aprovação;
5. Caso a venda tenha sido aprovada, a vending machine concede* os produtos. 

_(*) caso o equipamento apresente defeito, uma requisição de estorno será enfileirada e enviada assim que possível_

A comunicação entre cliente e servidor acontece nos passos 3 e 4.

Request3(**)
============

A requisição Request3 possui a seguinte estrutura, onde a unidade das colunas offset e tamanho é byte:

| Offset | Tamanho | Campo          | Descrição                                                    |
|:-------|:--------|:---------------|--------------------------------------------------------------|
| 0 | 4 | VendingMachineID | O código de identificação da VendingMachine que iniciou a venda. |
| 4 | 8 | ClientCardID | O código de identificação do cartão usado para pagamento da compra. |
| 12 | 1 | ItemsCount | A quantidade de itens vendidos. |
| 13 | n * 1 | Items | O código de cada produto da venda. |


*Exemplos:*
    0xff 0xff 0xff 0xff
    1-1111-2-

Request4(**)
============


(**) Para referência futura, esta requisição será nomeada.