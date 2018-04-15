#include "states/stateActivateChamber.h"
#include <Arduino.h>
#include "globals.h"

StateActivateChamber::StateActivateChamber(State *pNextState) : State(pNextState) {

}

void StateActivateChamber::enter() {
  gGlobals.gLcd.clear();
  gGlobals.gLcd.setCursor(5, 1);
  gGlobals.gLcd.print(F(" sirva-se"));

  gGlobals.gChamber.activateHelix(gGlobals.gCurProd.helix);

  goToInitialState();
}