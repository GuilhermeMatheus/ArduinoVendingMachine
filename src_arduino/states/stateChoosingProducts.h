#ifndef STATECHOOSINGPRODUCTS_H
#define STATECHOOSINGPRODUCTS_H

#include "state.h"

class StateChoosingProducts : public State {
public:
  StateChoosingProducts(State *pNextState);
  void enter();
  void begin();
};

#endif /* STATECHOOSINGPRODUCTS_H */