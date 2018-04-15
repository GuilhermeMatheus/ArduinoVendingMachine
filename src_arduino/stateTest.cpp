#include "states/stateTest.h"
#include <Arduino.h>
#include "globals.h"

StateTest::StateTest(State *pNextState) : State(pNextState) {

}

void StateTest::enter() {
  gGlobals.gLcd.setCursor(0, 3);
  gGlobals.gLcd.print(F("TEST STATE"));
  delay(2000);
  
  updateGlobalStateToNext();
}