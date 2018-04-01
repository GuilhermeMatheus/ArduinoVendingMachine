#ifndef CHAMBER_H
#define CHAMBER_H

#include <stdint.h>

class Chamber {
public:
  Chamber();
  void begin();
  uint8_t activateHelix(uint8_t col, uint8_t line);
};

#endif /* CHAMBER_H */