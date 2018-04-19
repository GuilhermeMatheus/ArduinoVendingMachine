#include "states/statePayment.h"
#include <Arduino.h>
#include "globals.h"
#include "hardware/helpers.h"

#define TIME_TO_SHOW_WALLET 9 
#define UX_SEC_MILLIS 1950

namespace {
  //   ____________________
  // 0|   Passe o cartao   
  // 1|        N...        
  // 2|--------------------
  // 3| 1 item  |  R$ 2,10
  bool tryGetWallet(Wallet &wallet) {
    Helpers::lcdWrite(3, 0, F("Passe o cartao"));
    Helpers::lcdWrite(9, 1, F("..."));
    Helpers::lcdWrite(0, 2, F("--------------------"));
    Helpers::lcdWrite(1, 3, F("1 item  |  R$"));
    gGlobals.gLcd.print(gGlobals.gCurProd.price);

    unsigned long now = millis();
    unsigned long timeout = now + (TIME_TO_SHOW_WALLET * UX_SEC_MILLIS);
    
    uint8_t lastTimeLeft = 0;
    while(now < timeout) {
      delay(150);
      uint8_t timeLeft = (timeout - now) / UX_SEC_MILLIS;

      if(lastTimeLeft != timeLeft) {
        gGlobals.gLcd.setCursor(8, 1);
        gGlobals.gLcd.print(timeLeft, DEC);
        lastTimeLeft = timeLeft;
      }

      if (gGlobals.gWalletProvider.tryGetWallet(wallet))
        return true;

      now = millis();
    }
    return false;
  }

}

StatePayment::StatePayment(State *pNextState) : State(pNextState) {
}

void StatePayment::enter() {
  gGlobals.gLcd.clear();

  if(tryGetWallet(gGlobals.gWallet)) {
    updateGlobalStateToNext();
  } else {
    Helpers::lcdWrite(2, 1, F("Compra cancelada"));
    delay(3000);
    goToInitialState();
  }
}