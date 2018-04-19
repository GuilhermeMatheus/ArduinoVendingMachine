#include "states/stateServerTransaction.h"
#include <Arduino.h>
#include "globals.h"
#include "hardware/helpers.h"

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

    gGlobals.gServerBridge.sale();
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