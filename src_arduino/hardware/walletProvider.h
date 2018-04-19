#ifndef WALLET_UID_PROVIDER_H
#define WALLET_UID_PROVIDER_H

#include <stdint.h>
#include "../entities/wallet.h"

class WalletProvider {
public:
  WalletProvider();
  
  void begin();
  bool tryGetWallet(Wallet &wallet);
};

#endif /* WALLET_UID_PROVIDER_H */