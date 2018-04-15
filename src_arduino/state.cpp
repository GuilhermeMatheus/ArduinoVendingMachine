#include "states/state.h"

State **State::_p_GlobalState = 0;
State *State::_p_InitialState = 0;

State::State(State *p_NextState) {
  _p_NextState = p_NextState;
};

void State::updateGlobalState(State *p_NextState) {
  *_p_GlobalState = p_NextState;
}

void State::setNextState(State *p_NextState) {
  _p_NextState = p_NextState;
};

void State::updateGlobalStateToNext() {
  updateGlobalState(_p_NextState);
}

void State::goToInitialState() {
  updateGlobalState(_p_InitialState);
}