#include "states/stateChoosingProducts.h"
#include <Arduino.h>
#include "globals.h"

namespace {

  int8_t getProductCode() {
    int8_t firstDigit;
    int8_t result = -1;

    gGlobals.lcd.setCursor(1, 1);
    gGlobals.lcd.blink();
    gGlobals.lcd.cursor();
    gGlobals.gKeyPad.waitInput();
    
    delay(500);

    if (gGlobals.gKeyPad.curCharIsCancel())
      return -1;

    if (gGlobals.gKeyPad.curCharIsDigit()) {
      firstDigit = gGlobals.gKeyPad.curCharAsDigit();
      if (firstDigit > 1) {
        result = firstDigit;
      } else {
        gGlobals.lcd.print(firstDigit);
        gGlobals.lcd.setCursor(2, 1);
        
        gGlobals.gKeyPad.waitInput();

        if (gGlobals.gKeyPad.curCharIsCancel())
          return -1;
        
        if (gGlobals.gKeyPad.curCharIsOk())
          return firstDigit;
        
        result = firstDigit * 10 + gGlobals.gKeyPad.curCharAsDigit();                                         
      }
    }

    return result;
  }

  int8_t askProduct() {
    gGlobals.lcd.clear();
    gGlobals.lcd.setCursor(1, 0);
    gGlobals.lcd.print(F("Escolha um produto"));
    gGlobals.lcd.setCursor(0, 2);
    gGlobals.lcd.print(F("--------------------"));
    gGlobals.lcd.setCursor(0, 3);
    gGlobals.lcd.print(F("*Ok"));
    gGlobals.lcd.setCursor(12, 3);
    gGlobals.lcd.print(F("#Cancela"));

    uint8_t productCode = getProductCode();
  }

}

StateChoosingProducts::StateChoosingProducts(State *pNextState) : State(pNextState) {

}

void StateChoosingProducts::enter() {
  //   ____________________
  // 0| Escolha um produto
  // 1| 2_
  // 2|--------------------
  // 3| *Ok        #Cancela
  
  delay(100);
  int product = askProduct();

  //updateGlobalStateToNext();
}