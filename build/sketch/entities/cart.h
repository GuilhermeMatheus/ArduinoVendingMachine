#include "product.h"

#ifndef CART_H
#define CART_H

class Cart
{
  int capacity;
  int cartPrice;
  int productsCount;
  Product** p_products;

public:
  Cart(int capacity);
  float GetCartPrice();
  void AddProduct(Product* p_product);
};

#endif /* CART_H */