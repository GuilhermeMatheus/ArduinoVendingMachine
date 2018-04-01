#ifndef IDLE_STATE_H
#define IDLE_STATE_H

#include "state.h"

class StateIdle : public State {
public:
  StateIdle(State *pNextState);
  void enter();
};

#endif /* IDLE_STATE_H */