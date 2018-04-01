#ifndef KEYPAD_H
#define KEYPAD_H

#include <stdint.h>

class KeyPad {
public:
  KeyPad(uint8_t c1, uint8_t c2, uint8_t c3);
  char waitForChar();
  int8_t waitForInt();
};

#endif /* KEYPAD_H */