#include "hardware/walletProvider.h"
#include <SPI.h>
#include <MFRC522.h>

// #define DEBUG

#define RST_PIN 9
#define SS_PIN 10  

namespace {
  MFRC522 mfrc522(SS_PIN, RST_PIN);
}

WalletProvider::WalletProvider() {
};

void WalletProvider::begin() {
  SPI.begin();
	mfrc522.PCD_Init();
}

bool WalletProvider::tryGetWallet(Wallet &wallet) {
  if (!mfrc522.PICC_IsNewCardPresent()) return false;
  if (!mfrc522.PICC_ReadCardSerial()) return false;

  wallet.UID[0] = mfrc522.uid.uidByte[0];
  wallet.UID[1] = mfrc522.uid.uidByte[1];
  wallet.UID[2] = mfrc522.uid.uidByte[2];
  wallet.UID[3] = mfrc522.uid.uidByte[3];

  return true;
}