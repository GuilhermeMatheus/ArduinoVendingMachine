#include "states/stateServerTransaction.h"
#include <Arduino.h>
#include "globals.h"

StateServerTransaction::StateServerTransaction(State *pNextState) : State(pNextState) {

}

void StateServerTransaction::enter() {
  gGlobals.gLcd.clear();
  gGlobals.gLcd.setCursor(5, 1);
  gGlobals.gLcd.print(F("servidor??"));

  delay(1500);
  updateGlobalStateToNext();
}