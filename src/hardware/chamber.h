#ifndef CHAMBER_H
#define CHAMBER_H

class Chamber
{
  bool takeOutDoorEmpty;

public:
  Chamber();
  int ActivateHelix(int helixNum);
  bool IsTakeOutDoorEmpty();
};

#endif /* CHAMBER_H */