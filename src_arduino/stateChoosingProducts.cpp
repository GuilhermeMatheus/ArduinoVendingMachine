#include "states/stateChoosingProducts.h"
#include "hardware/helpers.h"
#include <Arduino.h>
#include "globals.h"

// #define DEBUG

namespace {

  static int8_t getProductCode_Impl() {
    bool curCharIsDigit = gGlobals.gKeyPad.curCharIsDigit();
    
    if (!curCharIsDigit) return -1;

    int8_t firstDigit = gGlobals.gKeyPad.curCharAsDigit();
    if (firstDigit > 1) {
      return firstDigit;
    } else {
      gGlobals.gLcd.print(firstDigit);
      gGlobals.gLcd.setCursor(2, 1);
      
      gGlobals.gKeyPad.waitInput();

      if (gGlobals.gKeyPad.curCharIsCancel())
        return -1;
      
      if (gGlobals.gKeyPad.curCharIsOk())
        return firstDigit;
      
      return firstDigit * 10 + gGlobals.gKeyPad.curCharAsDigit();                                         
    }
  }

  static int8_t getProductCode() {
    int8_t result = -1;

    gGlobals.gLcd.setCursor(1, 1);
    gGlobals.gLcd.blink();
    gGlobals.gLcd.cursor();
    gGlobals.gKeyPad.waitInput();
    
    result = getProductCode_Impl();

    gGlobals.gLcd.noBlink();
    gGlobals.gLcd.noCursor();

    return result;
  }

  static int8_t askProductHelix() {
    //   ____________________
    // 0| Escolha um produto
    // 1| 0_
    // 2|--------------------
    // 3| *Ok        #Cancela
    Helpers::lcdWrite(1, 0, F("Escolha um produto"));
    Helpers::lcdWriteBottomMenu(HELPERS_CANCEL_BTN);

    return getProductCode();
  }

  // TODO: trocar isto por ponteiro
  static int8_t confirmProduct(Product prod) {

    gGlobals.gLcd.clear();
    
    gGlobals.gLcd.write(prod.name, sizeof(prod.name));
    gGlobals.gLcd.setCursor(0, 1);
    gGlobals.gLcd.print(F("R$ "));
    gGlobals.gLcd.print(prod.price);

    Helpers::lcdWriteBottomMenu();
  }

  static int16_t askProductId(Product &prod) {
    int8_t prodHelix = -1;
    do {
      prodHelix = askProductHelix();
    } while(prodHelix < 0);

    prod = gGlobals.gProductRepository.getByHelix(prodHelix);
    confirmProduct(prod);

    while(true) {
      gGlobals.gKeyPad.waitInput();
      if (gGlobals.gKeyPad.curCharIsCancel())
        return -1;
      if (gGlobals.gKeyPad.curCharIsOk())
        return prod.id;
    }
  }
}

StateChoosingProducts::StateChoosingProducts(State *pNextState) : State(pNextState) {
  
}

void StateChoosingProducts::begin() {
  /*
  for(uint8_t i = 1; i <= 16; i+=1) {
    Product prod;
    prod.id = i;
    prod.helix = i;
    prod.count = 10;
    prod.price = 12.34 * i;
    memcpy(prod.name, "AbcdefghifklmnopqrsA", sizeof(prod.name));
    prod.name[0] = i + '0';
    prod.name[19] = i + '0';
    gGlobals.gProductRepository.add(prod);
  }
  */
}

void StateChoosingProducts::enter() {
  gGlobals.gLcd.clear();

  int16_t prodId = askProductId(gGlobals.gCurProd);
  
#ifdef DEBUG
  gGlobals.gLcd.clear();
  gGlobals.gLcd.print(prodId, DEC);
  gGlobals.gLcd.setCursor(0, 1);
  gGlobals.gLcd.write(gGlobals.gCurProd.name, sizeof(gGlobals.gCurProd.name));
  delay(1000);
#endif

  if(prodId >= 0) {
    updateGlobalStateToNext();
  } else {
    goToInitialState();
  }
}