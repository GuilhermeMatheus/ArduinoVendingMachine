#ifndef SALE_RESPONSE_H
#define SALE_RESPONSE_H

#include <stdint.h>

class SaleResponse
{
public:
  uint16_t transactionId;
  uint8_t flagsResult;
  float creditAfterTransaction;
};


#endif /* SALE_RESPONSE_H */