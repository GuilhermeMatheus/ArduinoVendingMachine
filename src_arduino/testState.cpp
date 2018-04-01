#include "states/testStates.h"
#include <Arduino.h>
#include "globals.h"

TestState::TestState(State *pNextState) : State(pNextState) {

}

void TestState::enter() {
  gLcd.setCursor(0, 3);
  gLcd.print(F("TEST STATE"));
  delay(2000);
  
  updateGlobalStateToNext();
}