#ifndef Helpers_H
#define Helpers_H

#include <stdint.h>
#include "Arduino.h"

#define HELPERS_OK_BTN 1
#define HELPERS_CANCEL_BTN 2

class Helpers {
public:
  static void lcdWriteBottomMenu(uint8_t opt = HELPERS_OK_BTN | HELPERS_CANCEL_BTN);
  static void lcdWrite(uint8_t, uint8_t, const __FlashStringHelper *ifsh);
  static void lcdWriteBuildTime();
};

#endif /* Helpers_H */