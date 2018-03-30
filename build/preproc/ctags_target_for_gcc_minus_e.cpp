# 1 "c:\\Users\\costa\\Documents\\GitHub\\ArduinoVendingMachine\\src_arduino\\chamber.cpp"
# 1 "c:\\Users\\costa\\Documents\\GitHub\\ArduinoVendingMachine\\src_arduino\\chamber.cpp"
# 2 "c:\\Users\\costa\\Documents\\GitHub\\ArduinoVendingMachine\\src_arduino\\chamber.cpp" 2
# 3 "c:\\Users\\costa\\Documents\\GitHub\\ArduinoVendingMachine\\src_arduino\\chamber.cpp" 2
# 4 "c:\\Users\\costa\\Documents\\GitHub\\ArduinoVendingMachine\\src_arduino\\chamber.cpp" 2

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
# 1 "c:\\Users\\costa\\Documents\\GitHub\\ArduinoVendingMachine\\src_arduino\\program.ino"
# 2 "c:\\Users\\costa\\Documents\\GitHub\\ArduinoVendingMachine\\src_arduino\\program.ino" 2
# 3 "c:\\Users\\costa\\Documents\\GitHub\\ArduinoVendingMachine\\src_arduino\\program.ino" 2


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
