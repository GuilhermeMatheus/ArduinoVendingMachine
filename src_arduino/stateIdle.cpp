#include "states/stateIdle.h"
#include <Arduino.h>
#include "globals.h"
#include "hardware/helpers.h"

StateIdle::StateIdle(State *pNextState) : State(pNextState) {
}

void StateIdle::enter() {
  Helpers::lcdWrite(5, 1, F("Tecle algo"));
  Helpers::lcdWrite(4, 2, F("para comecar"));

  gGlobals.gKeyPad.waitInput();
  updateGlobalStateToNext();
}