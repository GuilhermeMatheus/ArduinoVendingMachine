#ifndef IDLE_STATE_H
#define IDLE_STATE_H

#include "state.h"

class IdleState : public State {
public:
  IdleState(State *pNextState);
  void enter();
};

#endif /* IDLE_STATE_H */