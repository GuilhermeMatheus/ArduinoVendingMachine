#ifndef SERVER_BRIDGE_H
#define SERVER_BRIDGE_H

#include "../entities/saleResponse.h"

class ServerBridge {
public:
  ServerBridge();
  void begin();
  void sale(SaleResponse &response);
  void verifyProductsUpdate();
};

#endif /* SERVER_BRIDGE_H */