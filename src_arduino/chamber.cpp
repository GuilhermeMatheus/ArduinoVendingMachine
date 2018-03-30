#include "hardware/chamber.h"
#include <Wire.h>
#include <Arduino.h>

namespace {
  const uint8_t addrSec1 = 0x3F;
  const uint8_t addrSec2 = 0x38;

  void writeInSection(uint8_t addrSec, uint8_t val) {
    Wire.beginTransmission(addrSec);
    Wire.write(val);
    Wire.endTransmission();
  }

  inline void clearSection(uint8_t addrSec) {
    writeInSection(addrSec, 0);
  }
}

Chamber::Chamber() {

}

void Chamber::begin() {
  Wire.begin();
  
  clearSection(addrSec1);
  clearSection(addrSec2);
}

uint8_t Chamber::activateHelix(uint8_t line, uint8_t col) {
  uint8_t idx;
  uint8_t val;
  uint8_t addr = addrSec1;

  if(line >= 2) {
    addr = addrSec2;
    line -= 2;
  }

  idx = line * 4 + col;
  val = 1 << idx;
  
  writeInSection(addr, val);  
  delay(500);
  clearSection(addr);
}