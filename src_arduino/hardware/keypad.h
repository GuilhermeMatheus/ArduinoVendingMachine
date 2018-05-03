#ifndef KEYPAD_H
#define KEYPAD_H

#include <stdint.h>

class KeyPad {
public:
  KeyPad(uint8_t c1, uint8_t c2, uint8_t c3);
  bool waitInput(uint16_t timeout);
  void waitInput();
  bool curCharIsOk();
  bool curCharIsCancel();
  bool curCharIsDigit();
  int8_t curCharAsDigit();
};

#endif /* KEYPAD_H */