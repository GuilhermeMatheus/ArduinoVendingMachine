#ifndef PRODUCT_H
#define PRODUCT_H

class Product
{
  float price;
  int helix;
  char* p_name;

public:
  Product(float price, char* name, int helix);
  float GetPrice();
  char* GetName();
};


#endif /* PRODUCT_H */