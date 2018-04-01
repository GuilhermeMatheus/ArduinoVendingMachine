#ifndef STATE_H
#define STATE_H

#include <stdint.h>

class State {
private:
  State *_p_NextState;

public:
  static State **_p_GlobalState;

  State(State *p_NextState) {
    _p_NextState = p_NextState;
  };
  virtual void enter() = 0;
  void updateGlobalState(State *p_NextState) {
    *_p_GlobalState = p_NextState;
  };
  void updateGlobalStateToNext() {
    updateGlobalState(_p_NextState);
  };
};

#endif /* STATE_H */