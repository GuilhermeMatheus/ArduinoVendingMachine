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
  //Aguardando transacao
  void connectToServer() {
    Helpers::lcdWrite(5, 1, F("Aguardando"));
    Helpers::lcdWrite(6, 2, F("servidor"));

    SaleResponse response;
    gGlobals.gServerBridge.sale(response);

    gGlobals.gLcd.setCursor(0, 3);
    gGlobals.gLcd.print(response.creditAfterTransaction);

    delay(2000);
  }
}

StateServerTransaction::StateServerTransaction(State *pNextState) : State(pNextState) {
}

void StateServerTransaction::enter() {
  gGlobals.gLcd.clear();
  gGlobals.gLcd.setCursor(5, 1);
  gGlobals.gLcd.print(F("servidor??"));

  connectToServer();

  delay(1500);
  updateGlobalStateToNext();
}