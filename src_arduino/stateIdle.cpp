#include "states/stateIdle.h"
#include <Arduino.h>
#include "globals.h"

StateIdle::StateIdle(State *pNextState) : State(pNextState) {

}

void StateIdle::enter() {
  gGlobals.gLcd.setCursor(5, 1);
  gGlobals.gLcd.print(F("Tecle algo"));
  
  gGlobals.gLcd.setCursor(4, 2);
  gGlobals.gLcd.print(F("para comecar"));

  gGlobals.gKeyPad.waitInput();
  updateGlobalStateToNext();
}