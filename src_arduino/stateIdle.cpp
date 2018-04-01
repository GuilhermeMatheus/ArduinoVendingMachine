#include "states/stateIdle.h"
#include <Arduino.h>
#include "globals.h"

StateIdle::StateIdle(State *pNextState) : State(pNextState) {

}

void StateIdle::enter() {
  gGlobals.lcd.setCursor(5, 1);
  gGlobals.lcd.print(F("Tecle algo"));
  
  gGlobals.lcd.setCursor(4, 2);
  gGlobals.lcd.print(F("para comecar"));

  gGlobals.gKeyPad.waitInput();
  updateGlobalStateToNext();
}