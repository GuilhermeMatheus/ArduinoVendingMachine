***Introdução***
==========

Este documento descreve a comunicação entre o servidor e a vending machine durante uma **transação de Atualização de tabela**.


***Atualização de tabela***
=====

A atualização de tabela acontece quando o servidor toma a iniciativa de alterar a tabela de produtos e preços em uma vending machine.

***Requisição de atualização de tabela***
=

A requisição de atualização da tabela possui a seguinte estrutura, onde a unidade das colunas offset e tamanho é byte:

| Offset     | Tamanho | Campo          | Tipo | Descrição                                                    |
|:-----------|:--------|:---------------|------|--------------------------------------------------------------|
| 0 | 1 | Início de requisição | Byte | A intenção de atualização de tabela. Veja mais [aqui](README.md#atualizacaoDePrecos). |
| 1 | 1 | ProductsCount | Byte | O número N de produtos enviados durante a requisição. |
| 2 + (i*27) | 1 | Product[i]::Id | Byte | Id do produto, compartilhado entre servidor e vending machine. |
| 3 + (i*27) | 1 | Product[i]::Helix | Byte | Hélice do produto na vending machine. |
| 4 + (i*27) | 1 | Product[i]::Count | Byte | Contagem de itens deste produto na máquina |
| 5 + (i*27) | 4 | Product[i]::Price | Float | Preço |
| 9 + (i*27) | 20 | Product[i]::Name | char[20] | Nome |
