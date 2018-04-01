#include "states/stateTest.h"
#include <Arduino.h>
#include "globals.h"

StateTest::StateTest(State *pNextState) : State(pNextState) {

}

void StateTest::enter() {
  gGlobals.lcd.setCursor(0, 3);
  gGlobals.lcd.print(F("TEST STATE"));
  delay(2000);
  
  updateGlobalStateToNext();
}