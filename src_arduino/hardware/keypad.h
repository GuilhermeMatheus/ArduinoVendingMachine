#ifndef KEYPAD_H
#define KEYPAD_H

class KeyPad
{

public:
  KeyPad();
  char WaitForInput(int timeout);
  int WaitForInteger(int timeout);
};

#endif /* KEYPAD_H */