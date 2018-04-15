#include "hardware/helpers.h"
#include "globals.h"

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