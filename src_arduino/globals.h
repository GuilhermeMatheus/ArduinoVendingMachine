#ifndef GLOBALS_H
#define GLOBALS_H

#include <LiquidCrystal.h>
#include "hardware/keypad.h"
#include "hardware/chamber.h"


struct Globals {
    LiquidCrystal lcd;
    KeyPad gKeyPad;
    Chamber gChamber;
};

extern struct Globals gGlobals;

#endif /* GLOBALS_H */