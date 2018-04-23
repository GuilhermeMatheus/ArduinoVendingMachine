#include "states/stateActivateChamber.h"
#include <Arduino.h>
#include "hardware/helpers.h"
#include "globals.h"

StateActivateChamber::StateActivateChamber(State *pNextState) : State(pNextState) {
}

//   ____________________
// 0| Saldo restante: 
// 0| R$120,00
// 0|--------------------
// 0|     Sirva-se
void StateActivateChamber::enter() {
  gGlobals.gLcd.clear();
  Helpers::lcdWrite(1, 0, F("Saldo restante"));
  Helpers::lcdWrite(1, 1, F("R$ "));
  gGlobals.gLcd.print(gGlobals.gSaleResponse.creditAfterTransaction);
  Helpers::lcdWrite(0, 2, F("--------------------"));
  Helpers::lcdWrite(5, 3, F("Sirva-se"));

  gGlobals.gChamber.activateHelix(gGlobals.gCurProd.helix);

  goToInitialState();
}