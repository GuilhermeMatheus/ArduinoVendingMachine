#ifndef STATE_H
#define STATE_H

#include <stdint.h>

class State {
private:
  State *_p_NextState;

public:
  static State **_p_GlobalState;
  static State *_p_InitialState;

  State(State *p_NextState);
  virtual void enter() = 0;
  void setNextState(State *p_NextState);
  void updateGlobalState(State *p_NextState);
  void updateGlobalStateToNext();
  void goToInitialState();
};

#endif /* STATE_H */