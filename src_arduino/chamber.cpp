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

  void clearSection(uint8_t addrSec) {
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

uint8_t Chamber::activateHelix(uint8_t helix) {
#ifdef DEBUG
  Serial.print(F("Ativando chamber helix="));
  Serial.print(helix, DEC);
  Serial.print(F("\r\n"));
#endif
  int8_t internalHelix = helix-1;
	int8_t line = internalHelix / 4;
	int8_t col = (helix % 4)-1;
	col = col == -1 ? 3 : col;

  activateHelix(line, col);
}

uint8_t Chamber::activateHelix(uint8_t line, uint8_t col) {
  uint8_t idx;
  uint8_t val;
  uint8_t addr = addrSec1;

#ifdef DEBUG
  Serial.print(F("Ativando chamber l="));
  Serial.print(line, DEC);
  Serial.print(F(";c="));
  Serial.print(col, DEC);
  Serial.print(F("\r\n"));
#endif

  if(line >= 2) {
    addr = addrSec2;
    line -= 2;
  }
 
  idx = line * 4 + col;
  val = 1 << idx;
  
  writeInSection(addr, val);  
  delay(3000);
  clearSection(addr);
}