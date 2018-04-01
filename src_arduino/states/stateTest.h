#ifndef TEXT_STATE_H
#define TEXT_STATE_H

#include "state.h"

class StateTest : public State {
public:
  StateTest(State *pNextState);
  void enter();
};

#endif /* TEXT_STATE_H */