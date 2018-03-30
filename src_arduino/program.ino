#include <LiquidCrystal.h>
#include "hardware/keypad.h"
#include "hardware/chamber.h"

LiquidCrystal lcd(6, 7, 2, 3, 4, 5);
KeyPad keyPad(A3, A2, A1);
Chamber chamber;

void setup() {
  lcd.begin(20, 4);
  chamber.begin();
}

void loop() {

  for(uint8_t i = 0; i < 4; i++) {
    for(uint8_t j = 0; j < 4; j++) {
      chamber.activateHelix(i, j);
    }
  }

  // char c = keyPad.waitForChar();
  // lcd.setCursor(2, 0);
  // lcd.print(c);
}