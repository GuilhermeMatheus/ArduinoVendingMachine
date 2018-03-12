#include "entities/cart.h"

Cart::Cart(int capacity) {
    this->capacity = capacity;
    this->productsCount = 0;
    this->cartPrice = 0;
    this->p_products = new Product*[capacity];
}

float Cart::getCartPrice() {
    return this->cartPrice;
}

void Cart::addProduct(Product* p_product) {
    if(productsCount <= capacity) {
        this->p_products[productsCount++] = p_product;
        this->cartPrice += p_product->getPrice();
    }
}
