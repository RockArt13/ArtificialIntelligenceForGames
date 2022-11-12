using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenIdleState : State
{
    private readonly GreenPlayer greenPlayer;

    public GreenIdleState(GreenPlayer target)
    {
        this.greenPlayer = target;
    }

    public override State Execute()
    {
        if (greenPlayer.captured)
        {
            return new GreenEscortedState(greenPlayer);
        }
        else
        {
            return new GreenRunningState(greenPlayer);
        }
    }
}
