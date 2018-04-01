#include <LiquidCrystal.h>
#include "globals.h"
#include "states/state.h"
#include "states/stateIdle.h"
#include "states/stateTest.h"

static State *gStateCurr = NULL;

static StateTest stateTest(NULL);
static StateIdle stateIdle(&stateTest);

static void beforeNextState();

void setup() {
  gGlobals.lcd.begin(20, 4);
  gGlobals.gChamber.begin();

  State::_p_GlobalState = &gStateCurr;
}

void loop() {
  if(gStateCurr == NULL) {
    gStateCurr = &stateIdle;
  }

  beforeNextState();
  gStateCurr->enter();
}

static void beforeNextState() {
  gGlobals.lcd.clear();
}