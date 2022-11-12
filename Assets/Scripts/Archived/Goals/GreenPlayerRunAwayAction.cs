using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenPlayerRunAwayAction : Action
{
  GreenPlayer player;

  public override void Execute()
  {
    //player.Move();
  }

  public override float PriorityChange(Goal g)
  {
    switch (g.name)
    {
      case Goal.StayFree:
        return 4f;
      case Goal.StayRested:
        return -4f;
      default:
        return 0f;
    }
  }
}
