#ifndef PRODUCT_H
#define PRODUCT_H

#include <stdint.h>

class Product
{
public:
  uint8_t id; // server id
  uint8_t helix;
  uint8_t count;
  float price;
  char name[20]; 
};


#endif /* PRODUCT_H */