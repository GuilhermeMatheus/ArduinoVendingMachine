#include "entities/product.h"

Product::Product(float price, char* p_name) {
  this->p_name = p_name;
  this->price = price;
}

float Product::getPrice() {
  return this->price;
}

char* Product::getName() {
  return this->p_name;
}