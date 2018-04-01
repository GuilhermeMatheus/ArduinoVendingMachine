#include "states/idleState.h"
#include <Arduino.h>
#include "globals.h"

IdleState::IdleState(State *pNextState) : State(pNextState) {

}

void IdleState::enter() {
  gLcd.setCursor(3, 3);
  gLcd.print(F("IDLE"));


  delay(10000);
  updateGlobalStateToNext();
}