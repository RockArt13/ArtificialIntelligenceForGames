using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleEscortingState : State
{
    private readonly GreenPlayer greenPlayer = null;
    private readonly PurplePlayer purplePlayer;

  public PurpleEscortingState(PurplePlayer chaser, GreenPlayer target)
  {
    this.purplePlayer = chaser;
    this.greenPlayer = target;
  }

    public override State Execute()
    {

        if (purplePlayer.IsJailed(greenPlayer))
        {
            return new PurpleIdleState(purplePlayer);
        }
        else
        {
            return this;
        }
    }


}
