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
  gGlobals.lcd.begin(20, 4);
  gGlobals.gChamber.begin();

  State::_p_GlobalState = &gStateCurr;
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
  gGlobals.lcd.clear();
  gGlobals.lcd.noCursor();
}


/*
while(true) {
  gGlobals.lcd.clear();
  gGlobals.gKeyPad.waitInput();

  gGlobals.lcd.setCursor(0, 0);
  gGlobals.lcd.write(gGlobals.gKeyPad.curCharIsCancel() ? "Cancelar" : "Nao cancelar");
  gGlobals.lcd.setCursor(0, 1);
  gGlobals.lcd.write(gGlobals.gKeyPad.curCharIsOk() ? "Ok" : "Nao ok");
  gGlobals.lcd.setCursor(0, 2);
  gGlobals.lcd.write(gGlobals.gKeyPad.curCharIsDigit() ? "Eh digito" : "Nao eh digito");
  gGlobals.lcd.setCursor(0, 3);
  gGlobals.lcd.print(gGlobals.gKeyPad.curCharAsDigit());

  delay(3000);
}
*/
