#include "globals.h"
#include "states/state.h"
#include "states/idleState.h"
#include "states/testStates.h"

static State *gStateCurr = NULL;
State **State::_p_GlobalState = 0;

static TestState testState(NULL);
static IdleState idleState(&testState);

static void beforeNextState();

void setup() {
  gLcd.begin(20, 4);
  gChamber.begin();

  State::_p_GlobalState = &gStateCurr;
}

void loop() {
  if(gStateCurr == NULL) {
    gStateCurr = &idleState;
  }

  beforeNextState();
  gStateCurr->enter();
}

static void beforeNextState() {
  gLcd.clear();
}