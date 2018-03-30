#include "hardware/keypad.h"
#include <Arduino.h>

namespace {
  uint8_t col1;
  uint8_t col2;
  uint8_t col3;
  uint8_t col4;

  char charMatrix[][4] = {
    { '1', '4', '7', '*' },
    { '2', '5', '8', '0' },
    { '3', '6', '9', '#' }
  };

  inline bool inRange(int value, uint16_t pivot) {
    return (value > pivot - 150) && (value < pivot + 150);
  }

  int8_t getColumnState(uint8_t col) {
    int value = analogRead(col);
    delay(250);

    if(inRange(value, 242))
      return 3;
    
    if(inRange(value, 507))
      return 2;
    
    if(inRange(value, 765))
      return 1;

    if(inRange(value, 1013))
      return 0;
    
    return -1;
  }

  char getKeyPadState() {
    int8_t state;

    state = getColumnState(col1);
    if(getColumnState(col1) >= 0)
      return charMatrix[0][state];
    
    state = getColumnState(col2);
    if(getColumnState(col2) >= 0)
      return charMatrix[1][state];
    
    state = getColumnState(col3);
    if(getColumnState(col3) >= 0)
      return charMatrix[2][state];
    
    return ' ';
  }
}

KeyPad::KeyPad(uint8_t c1, uint8_t c2, uint8_t c3) {
  col1 = c1;
  col2 = c2;
  col3 = c3;
}

char KeyPad::waitForChar() {
  char state;
  
  do {
    state = getKeyPadState();
  } while(state == ' ');
  
  return state;
}

int8_t KeyPad::waitForInt() {
  return waitForChar() - 48;
}

