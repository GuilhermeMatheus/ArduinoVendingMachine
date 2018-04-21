#ifndef SALE_RESPONSE_H
#define SALE_RESPONSE_H

#include <stdint.h>

/*
 * Attention!
 * See alignment and flag values in docs/Protocolo/Venda.md
 */

#define FLAG_SUCCESS            (1<<7)
#define FLAG_INSUFFICIENT_FUNDS (1<<6)
#define FLAG_INVALID_PRICE      (1<<5)
#define FLAG_USER_NOT_FOUND     (1<<4)
#define FLAG_PRODUCT_NOT_FOUND  (1<<3)

class SaleResponse
{
public:
  uint16_t transactionId;
  uint8_t flagsResult;
  float creditAfterTransaction;
};


#endif /* SALE_RESPONSE_H */