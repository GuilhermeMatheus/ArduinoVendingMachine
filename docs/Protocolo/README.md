***Introdução***
==========

Este documento descreve o protocolo de aplicação entre o servidor e a vending machine. 

O protocolo HTTP oferece muitos recursos dispensáveis para este projeto. Além disso, o overhead para se iniciar uma requisição HTTP é grande. O custo do HTTP não é justificado pelos recursos necessários pela ArduinoVendingMachine e, por esta razão, usamos o protocolo de aplicação definido aqui.

Endianness
=====

Todos os valores numéricos são representados na ordem **big-endian**.

Ações
=====

As ações permitidas por este protocolo são restritas. **Toda comunicação possui uma intenção única, definida no primeiro byte dos dados transferidos pelo iniciador**. 

| Valor do primeiro Byte | Iniciador | Tipo de pedido |
|:---------------------  |:--------  |:-------------- |
|0x00  | VendingMachine  | [Venda](Venda.md)
|0x01  | VendingMachine  | [Estorno](Estorno.md)
|0x02  | Servidor  | [Atualização de preços](AtualizacaoDePrecos.md)
|0x03  | VendingMachine  | [Inicialização de máquina](InicializacaoMaquina.md)
