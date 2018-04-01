#include "states/testStates.h"
#include <Arduino.h>
#include "globals.h"

TestState::TestState(State *pNextState) : State(pNextState) {

}

void TestState::enter() {
  gGlobals.lcd.setCursor(0, 3);
  gGlobals.lcd.print(F("TEST STATE"));
  delay(2000);
  
  updateGlobalStateToNext();
}