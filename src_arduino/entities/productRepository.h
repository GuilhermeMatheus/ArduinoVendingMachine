#ifndef PRODUCT_REPOSITORY_H
#define PRODUCT_REPOSITORY_H

#include <stdint.h>
#include "product.h"

class ProductRepository
{
public:
  ProductRepository();
  Product getById(char id);
  Product getByHelix(char helix);
  void add(Product product);
  void update(Product product);
  void removeById(char id);
};

#endif /* PRODUCT_REPOSITORY_H */