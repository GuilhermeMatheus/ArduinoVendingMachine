#include "states/stateChoosingProducts.h"
#include "hardware/helpers.h"
#include <Arduino.h>
#include "globals.h"

namespace {

  static int8_t getProductCode_Impl() {
    bool curCharIsDigit = gGlobals.gKeyPad.curCharIsDigit();
    
    if (!curCharIsDigit) return -1;

    int8_t firstDigit = gGlobals.gKeyPad.curCharAsDigit();
    if (firstDigit > 1) {
      return firstDigit;
    } else {
      gGlobals.gLcd.print(firstDigit);

      Helpers::lcdWriteBottomMenu();

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
    
    prodHelix = askProductHelix();
    
    if (prodHelix < 0)
        return -1;

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
  uint8_t item_idx = 0;
  
  Product prod;
  prod.count = 10;

  prod.id = item_idx;
  prod.helix = item_idx++;
  memcpy(prod.name, "Bolacha Passa-tempo ", sizeof(prod.name));
  prod.price = 4.55;
  gGlobals.gProductRepository.add(prod);

  prod.id = item_idx;
  prod.helix = item_idx++;
  memcpy(prod.name, "Bolacha Negresco    ", sizeof(prod.name));
  prod.price = 4.50;
  gGlobals.gProductRepository.add(prod);

  prod.id = item_idx;
  prod.helix = item_idx++;
  memcpy(prod.name, "Doritos Frito/Assado", sizeof(prod.name));
  prod.price = 6;
  gGlobals.gProductRepository.add(prod);

  prod.id = item_idx;
  prod.helix = item_idx++;
  memcpy(prod.name, "Twix cobertura dupla", sizeof(prod.name));
  prod.price = 3.50;
  gGlobals.gProductRepository.add(prod);

  prod.id = item_idx;
  prod.helix = item_idx++;
  memcpy(prod.name, "Barra de cereal diet", sizeof(prod.name));
  prod.price = 1.50;
  gGlobals.gProductRepository.add(prod);

  prod.id = item_idx;
  prod.helix = item_idx++;
  memcpy(prod.name, "Snicker             ", sizeof(prod.name));
  prod.price = 2.50;
  gGlobals.gProductRepository.add(prod);

  prod.id = item_idx;
  prod.helix = item_idx++;
  memcpy(prod.name, "Bolo floresta negra ", sizeof(prod.name));
  prod.price = 7;
  gGlobals.gProductRepository.add(prod);

  prod.id = item_idx;
  prod.helix = item_idx++;
  memcpy(prod.name, "M&Ms                ", sizeof(prod.name));
  prod.price = 3;
  gGlobals.gProductRepository.add(prod);

  prod.id = item_idx;
  prod.helix = item_idx++;
  memcpy(prod.name, "Refrigerante Pepsi  ", sizeof(prod.name));
  prod.price = 5;
  gGlobals.gProductRepository.add(prod);

  prod.id = item_idx;
  prod.helix = item_idx++;
  memcpy(prod.name, "Coca-cola zero      ", sizeof(prod.name));
  prod.price = 5;
  gGlobals.gProductRepository.add(prod);

  prod.id = item_idx;
  prod.helix = item_idx++;
  memcpy(prod.name, "Suco de uva DelValle", sizeof(prod.name));
  prod.price = 4.50;
  gGlobals.gProductRepository.add(prod);

  prod.id = item_idx;
  prod.helix = item_idx++;
  memcpy(prod.name, "Suco de laranja Ades", sizeof(prod.name));
  prod.price = 4.50;
  gGlobals.gProductRepository.add(prod);

  prod.id = item_idx;
  prod.helix = item_idx++;
  memcpy(prod.name, "Lanche natural vegan", sizeof(prod.name));
  prod.price = 6.50;
  gGlobals.gProductRepository.add(prod);

  prod.id = item_idx;
  prod.helix = item_idx++;
  memcpy(prod.name, "Lanche peito de peru", sizeof(prod.name));
  prod.price = 6.50;
  gGlobals.gProductRepository.add(prod);

  prod.id = item_idx;
  prod.helix = item_idx++;
  memcpy(prod.name, "Hamburguer X-Tudo   ", sizeof(prod.name));
  prod.price = 6.50;
  gGlobals.gProductRepository.add(prod);

  prod.id = item_idx;
  prod.helix = item_idx++;
  memcpy(prod.name, "Bolacha Negresco    ", sizeof(prod.name));
  prod.price = 4.50;
  gGlobals.gProductRepository.add(prod);

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