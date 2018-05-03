#include "states/stateIdle.h"
#include <Arduino.h>
#include "globals.h"
#include "hardware/helpers.h"
#include "entities/productRepository.h"

namespace {
  void showUpdateMessage() {
    Helpers::lcdWrite(5, 1, F("Realizando"));
    Helpers::lcdWrite(4, 2, F("Atualizacao."));
  }

  void showWelcomeMessage() {
    Helpers::lcdWrite(5, 1, F("Tecle algo"));
    Helpers::lcdWrite(4, 2, F("para comecar"));
  }

  void updateHandler(char *data) { // char productData[27*16];
    showUpdateMessage();

    uint8_t productsCount = data[1];

    for(uint8_t i = 2; i <= 16; i += sizeof(Product)) {
      union {
        Product prod;
        char bytes[sizeof(Product)];
      } itemData;

      memcpy(&data[i], itemData.bytes, sizeof(Product));
      gGlobals.gProductRepository.add(itemData.prod);
    }

    showWelcomeMessage();
  }
}

StateIdle::StateIdle(State *pNextState) : State(pNextState) {
}

void StateIdle::enter() {
  showWelcomeMessage();

  while(true) {
    // gGlobals.gServerBridge.verifyProductsUpdate(500, &updateHandler);

    if (gGlobals.gKeyPad.waitInput(1500)) {
      break;
    }
  }

  updateGlobalStateToNext();
}