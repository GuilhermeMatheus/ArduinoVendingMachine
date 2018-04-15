#ifndef GLOBALS_H
#define GLOBALS_H

#include <LiquidCrystal.h>
#include "hardware/keypad.h"
#include "hardware/chamber.h"
#include "entities/product.h"
#include "entities/productRepository.h"
#include "server/serverBridge.h"

struct Globals {
    Product gCurProd;
    LiquidCrystal gLcd;
    KeyPad gKeyPad;
    Chamber gChamber;
    ProductRepository gProductRepository;
    ServerBridge gServerBridge;
};

extern struct Globals gGlobals;

#endif /* GLOBALS_H */