#ifndef PAYMENT_STATE_H
#define PAYMENT_STATE_H

#include "state.h"

class StatePayment : public State {
public:
  StatePayment(State *pNextState);
  void enter();
};

#endif /* PAYMENT_STATE_H */