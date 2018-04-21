#include "states/stateServerTransaction.h"
#include <Arduino.h>
#include "globals.h"
#include "hardware/helpers.h"
#include "entities/saleResponse.h"

namespace {
  //   ____________________
  // 0|
  // 1|     Aguardando
  // 2|      servidor
  // 3|
  void connectToServer(SaleResponse &response) {
    Helpers::lcdWrite(5, 1, F("Aguardando"));
    Helpers::lcdWrite(6, 2, F("servidor"));

    gGlobals.gServerBridge.sale(response);
  }

  void showError(uint8_t flag) {
    gGlobals.gLcd.clear();
    
    if ((flag & FLAG_INSUFFICIENT_FUNDS) == FLAG_INSUFFICIENT_FUNDS) {
      Helpers::lcdWrite(1, 1, F("Saldo insuficiente"));
    } else if ((flag & FLAG_USER_NOT_FOUND) == FLAG_USER_NOT_FOUND) {
      Helpers::lcdWrite(5, 1, F("Cartao nao"));
      Helpers::lcdWrite(5, 2, F("cadastrado"));
    } else {
      Helpers::lcdWrite(0, 0, F("Erro interno: "));
      gGlobals.gLcd.print(flag, HEX);
    }

    delay(4000);
  }
}

StateServerTransaction::StateServerTransaction(State *pNextState) : State(pNextState) {
}

void StateServerTransaction::enter() {
  gGlobals.gLcd.clear();
  
  connectToServer(gGlobals.gSaleResponse);

  if((gGlobals.gSaleResponse.flagsResult & FLAG_SUCCESS) == FLAG_SUCCESS) {
    updateGlobalStateToNext();
    return;
  }
  
  showError(gGlobals.gSaleResponse.flagsResult);
  goToInitialState();
}