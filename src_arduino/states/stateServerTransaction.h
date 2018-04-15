#ifndef SERVER_TRANSACTION_STATE_H
#define SERVER_TRANSACTION_STATE_H

#include "state.h"

class StateServerTransaction : public State {
public:
  StateServerTransaction(State *pNextState);
  void enter();
};

#endif /* SERVER_TRANSACTION_STATE_H */