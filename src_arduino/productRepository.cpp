#include "entities/productRepository.h"
#include <Arduino.h>
#include <EEPROM.h>

namespace {
  static Product fromBytes(char* addr) {
    union {
      Product prod;
      char bytes[sizeof(Product)];
    } data;

    memcpy(data.bytes, addr, sizeof(Product));

    return data.prod;
  }

  static void toBytes(Product prod, char* dest) {
    union {
      Product prod;
      char bytes[sizeof(Product)];
    } data;

    data.prod = prod;

    memcpy(dest, data.bytes, sizeof(Product));
  }

  static Product getProductInAddr(uint16_t addr) {
    char bytes[sizeof(Product)];

    for(uint8_t i = 0; i < sizeof(Product); i++) {
      bytes[i] = EEPROM.read(addr+i);
    }

    return fromBytes(bytes);
  }

  static void saveProductInAddr(Product prod, uint16_t addr) {
    char prodBytes[sizeof(Product)];
    toBytes(prod, prodBytes);

#ifdef DEBUG
    Serial.print(F("Escrevendo em "));
    Serial.print(addr, DEC);
    Serial.print("\r\n");

    for(int8_t i = 0; i < sizeof(Product); i++) {
      Serial.print("  ");
      Serial.print(i, DEC);
      Serial.print("  |");
    }
    Serial.print("\r\n");
#endif

    for(int8_t i = 0; i < sizeof(Product); i++) {
      EEPROM.write(addr+i, prodBytes[i]);
#ifdef DEBUG
      Serial.print("  ");
      Serial.print(prodBytes[i], HEX);
      Serial.print("  |");
#endif
    }
#ifdef DEBUG
    Serial.print("\r\n");
#endif
  }

  static uint16_t findAddrByField(uint8_t fieldOffset, uint8_t fieldByteValue) {
    uint16_t memLimit = sizeof(Product) * 16 + fieldOffset;
    
#ifdef DEBUG
    Serial.print(F("Procurando "));
    Serial.print(fieldByteValue, DEC);
    Serial.print("\r\n");
#endif
    
    for( uint16_t i = fieldOffset; i < memLimit; i += sizeof(Product)) {
      uint8_t val = EEPROM.read(i);

#ifdef DEBUG
      Serial.print(F("Encontrado em "));
      Serial.print(i, DEC);
      Serial.print(F(" valor "));
      Serial.print(val, DEC);
      Serial.print("\r\n");
#endif

      if (val == fieldByteValue)
        return i - fieldOffset;
    }

    return -1;
  }
}

ProductRepository::ProductRepository() {

}

Product ProductRepository::getByHelix(char helix) {
  uint16_t addr = findAddrByField(0, helix);
  return getProductInAddr(addr);
}

Product ProductRepository::getById(char id) {
  uint16_t addr = findAddrByField(0, id);
  return getProductInAddr(addr);
}

void ProductRepository::update(Product product) {

}
void ProductRepository::removeById(char id) {

}
void ProductRepository::add(Product product) {
  saveProductInAddr(product, product.id * sizeof(Product));
}