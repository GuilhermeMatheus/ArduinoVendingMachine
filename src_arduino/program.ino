#include <LiquidCrystal.h>
#include "globals.h"
#include "states/state.h"
#include "states/stateIdle.h"
#include "states/stateChoosingProducts.h"
#include "states/statePayment.h"
#include "states/stateServerTransaction.h"
#include "states/stateActivateChamber.h"
#include "hardware/helpers.h"

static State *gStateCurr = NULL;

static StateActivateChamber stateActivateChamber(NULL);
static StateServerTransaction stateServerTransaction(&stateActivateChamber);
static StatePayment statePayment(&stateServerTransaction);
static StateChoosingProducts stateChoosingProducts(&statePayment);
static StateIdle stateIdle(&stateChoosingProducts);

static void beforeNextState();

void setup() {
  Serial.begin(9600);
  gGlobals.gLcd.begin(20, 4);

  Helpers::lcdWriteBuildTime();
  gGlobals.gChamber.begin();
  gGlobals.gServerBridge.begin();
  gGlobals.gWalletProvider.begin();

  stateChoosingProducts.begin();

  State::_p_GlobalState = &gStateCurr;
  State::_p_InitialState = &stateIdle;
}

void loop() {
  if(gStateCurr == NULL) {
    gStateCurr = &stateIdle;
  }

  beforeNextState();
  gStateCurr->enter();
}

static void beforeNextState() {
  gGlobals.gLcd.clear();
  gGlobals.gLcd.noCursor();
}
