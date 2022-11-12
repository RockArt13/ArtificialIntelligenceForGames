using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenEscortedState : State
{
    private readonly GreenPlayer greenPlayer;
   
    public GreenEscortedState(GreenPlayer target)
    {
        this.greenPlayer = target;
    }

    public override State Execute()
    {

        if(greenPlayer.IsJailed(greenPlayer))
        {
            return new GreenJailedState(greenPlayer);
        }

        else
        {
            return this;
        }
    }
}
