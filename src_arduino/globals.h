#ifndef GLOBALS_H
#define GLOBALS_H

#include <LiquidCrystal.h>
#include "hardware/keypad.h"
#include "hardware/chamber.h"
#include "hardware/walletProvider.h"
#include "entities/product.h"
#include "entities/productRepository.h"
#include "entities/saleResponse.h"
#include "server/serverBridge.h"

struct Globals {
    uint16_t gMachineId;
    Wallet gWallet;
    Product gCurProd;
    SaleResponse gSaleResponse;
    LiquidCrystal gLcd;
    KeyPad gKeyPad;
    Chamber gChamber;
    ProductRepository gProductRepository;
    ServerBridge gServerBridge;
    WalletProvider gWalletProvider;
};

extern struct Globals gGlobals;

#endif /* GLOBALS_H */