#include "states/statePayment.h"
#include <Arduino.h>
#include "globals.h"

StatePayment::StatePayment(State *pNextState) : State(pNextState) {

}

void StatePayment::enter() {
  gGlobals.gLcd.clear();
  gGlobals.gLcd.setCursor(5, 1);
  gGlobals.gLcd.print(F("voce pagou"));

  delay(1500);
  updateGlobalStateToNext();
}