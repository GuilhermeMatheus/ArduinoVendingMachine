#include "hardware/helpers.h"
#include "globals.h"

// #define DEBUG

void Helpers::lcdWriteBottomMenu(uint8_t opt) {  
  Helpers::lcdWrite(0, 2, F("--------------------"));
  
  if((opt & HELPERS_OK_BTN) == HELPERS_OK_BTN)
    Helpers::lcdWrite(0, 3, F("*Ok"));
  
  if((opt & HELPERS_CANCEL_BTN) == HELPERS_CANCEL_BTN)
    Helpers::lcdWrite(12, 3, F("#Cancela"));
}

void Helpers::lcdWrite(uint8_t col, uint8_t line, const __FlashStringHelper *ifsh) {
  gGlobals.gLcd.setCursor(col, line);
  gGlobals.gLcd.print(ifsh);
}

void Helpers::lcdWriteBuildTime() {
#ifdef DEBUG
  lcdWrite(9, 2, F(__DATE__));
  lcdWrite(12, 3, F(__TIME__));
  lcdWrite(0, 3, F("ID:"));
  gGlobals.gLcd.print(gGlobals.gMachineId);
#endif
}