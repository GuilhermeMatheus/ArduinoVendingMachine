#ifndef GLOBALS_H
#define GLOBALS_H

#include <LiquidCrystal.h>
#include "hardware/keypad.h"
#include "hardware/chamber.h"

static LiquidCrystal gLcd(6, 7, 2, 3, 4, 5);
static KeyPad gKeyPad(A3, A2, A1);
static Chamber gChamber;

#endif /* GLOBALS_H */