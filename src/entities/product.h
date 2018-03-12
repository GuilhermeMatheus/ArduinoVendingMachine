#ifndef PRODUCT_H
#define PRODUCT_H

class Product
{
  float price;
  char* p_name;

public:
  Product(float price, char* name);
  float getPrice();
  char* getName();
};


#endif /* PRODUCT_H */