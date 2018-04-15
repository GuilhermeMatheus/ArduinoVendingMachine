#include "globals.h"
#include <Arduino.h>

struct Globals gGlobals = { 
  Product(),
  LiquidCrystal(6, 7, 2, 3, 4, 5),
  KeyPad(A3, A2, A1),
  Chamber(),
  ProductRepository()
};
