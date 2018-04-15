#ifndef ACTIVATE_CHAMBER_STATE_H
#define ACTIVATE_CHAMBER_STATE_H

#include "state.h"

class StateActivateChamber : public State {
public:
  StateActivateChamber(State *pNextState);
  void enter();
};

#endif /* ACTIVATE_CHAMBER_STATE_H */