#ifndef SERVER_BRIDGE_H
#define SERVER_BRIDGE_H

#include <stdint.h>
#include "../entities/saleResponse.h"

class ServerBridge {
public:
  ServerBridge();
  void begin();
  void sale(SaleResponse &response);
  void setServerMode();
  bool verifyProductsUpdate(uint16_t timeout, void (*f_handler)(char *));
};

#endif /* SERVER_BRIDGE_H */