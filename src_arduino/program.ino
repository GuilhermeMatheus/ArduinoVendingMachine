#define DEBUG
#undef DEBUG

#include <LiquidCrystal.h>
#include "globals.h"
#include "states/state.h"
#include "states/stateIdle.h"
#include "states/stateTest.h"
#include "states/stateChoosingProducts.h"

static State *gStateCurr = NULL;

static StateTest stateTest(NULL);
static StateChoosingProducts stateChoosingProducts(&stateTest);
static StateIdle stateIdle(&stateChoosingProducts);

static void beforeNextState();

void setup() {
  Serial.begin(9600);
  gGlobals.gLcd.begin(20, 4);
  gGlobals.gChamber.begin();

  stateChoosingProducts.begin();
  //stateChoosingProducts.setNextState(&stateIdle);

  State::_p_GlobalState = &gStateCurr;
  State::_p_InitialState = &stateIdle;
}

void loop() {

  if(gStateCurr == NULL) {
    //gStateCurr = &stateIdle;
    gStateCurr = &stateChoosingProducts;
  }

  beforeNextState();
  gStateCurr->enter();
}

static void beforeNextState() {
  gGlobals.gLcd.clear();
  gGlobals.gLcd.noCursor();
}

/*
while(true) {
  gGlobals.gLcd.clear();
  gGlobals.gKeyPad.waitInput();

  gGlobals.gLcd.setCursor(0, 0);
  gGlobals.gLcd.write(gGlobals.gKeyPad.curCharIsCancel() ? "Cancelar" : "Nao cancelar");
  gGlobals.gLcd.setCursor(0, 1);
  gGlobals.gLcd.write(gGlobals.gKeyPad.curCharIsOk() ? "Ok" : "Nao ok");
  gGlobals.gLcd.setCursor(0, 2);
  gGlobals.gLcd.write(gGlobals.gKeyPad.curCharIsDigit() ? "Eh digito" : "Nao eh digito");
  gGlobals.gLcd.setCursor(0, 3);
  gGlobals.gLcd.print(gGlobals.gKeyPad.curCharAsDigit());

  delay(3000);
}
*/
