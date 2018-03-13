#include "entities/product.h"

Product::Product(float price, char* p_name, int helix) {
  this->p_name = p_name;
  this->price = price;
  this->helix = helix;
}

float Product::GetPrice() {
  return this->price;
}

char* Product::GetName() {
  return this->p_name;
}