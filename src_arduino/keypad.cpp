#include "hardware/keypad.h"
#include <Arduino.h>

namespace {
  uint8_t col1;
  uint8_t col2;
  uint8_t col3;
  uint8_t col4;

  char currentChar;
  int8_t lastCol = -1;
  int8_t lastValue = -1;

  const char charMatrix[][4] PROGMEM = {
    { '1', '4', '7', '*' },
    { '2', '5', '8', '0' },
    { '3', '6', '9', '#' }
  };

  bool inRange(int value, uint16_t pivot) {
    return (value > pivot - 150) && (value < pivot + 150);
  }

  int8_t getColumnState(uint8_t col) {
    int8_t result = -1;
    int value = analogRead(col);
    delay(100);

    if(inRange(value, 242))
      result = 3;
    else if(inRange(value, 507))
      result = 2;
    else if(inRange(value, 765))
      result = 1;
    else if(inRange(value, 1013))
      result = 0;

    if(lastCol == col) {
      if(lastValue == result) {
        return -1;
      }

      lastValue = result;
    } else if(result > -1) {
      lastCol = col;
      lastValue = result;
    }

    return result;
  }

  char getKeyPadState() {
    int8_t state;

    state = getColumnState(col1);
    if(state >= 0)
      return pgm_read_byte(&(charMatrix[0][state]));;
    
    state = getColumnState(col2);
    if(state >= 0)
      return pgm_read_byte(&(charMatrix[1][state]));;
    
    state = getColumnState(col3);
    if(state >= 0)
      return pgm_read_byte(&(charMatrix[2][state]));;
    
    return ' ';
  }
}

KeyPad::KeyPad(uint8_t c1, uint8_t c2, uint8_t c3) {
  col1 = c1;
  col2 = c2;
  col3 = c3;
  currentChar = ' ';
}

void KeyPad::waitInput(){ 
  currentChar = ' ';

  do {
    currentChar = getKeyPadState();
  } while(currentChar == ' ');
}

bool KeyPad::curCharIsOk(){ 
  return currentChar == '*';
}

bool KeyPad::curCharIsCancel(){ 
  return currentChar == '#';
}

bool KeyPad::curCharIsDigit(){ 
  return ('0' <= currentChar && currentChar <= '9');
}

int8_t KeyPad::curCharAsDigit(){ 
  return (int8_t)(currentChar - '0');
}
