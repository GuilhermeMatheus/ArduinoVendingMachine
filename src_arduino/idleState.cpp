#include "states/idleState.h"
#include <Arduino.h>
#include "globals.h"

IdleState::IdleState(State *pNextState) : State(pNextState) {

}

void IdleState::enter() {
  gGlobals.lcd.setCursor(5, 1);
  gGlobals.lcd.print(F("Tecle algo"));
  
  gGlobals.lcd.setCursor(4, 2);
  gGlobals.lcd.print(F("para comecar"));

  delay(10000);
  updateGlobalStateToNext();
}