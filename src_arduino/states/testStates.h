#ifndef TEXT_STATE_H
#define TEXT_STATE_H

#include "state.h"

class TestState : public State {
public:
  TestState(State *pNextState);
  void enter();
};

#endif /* TEXT_STATE_H */