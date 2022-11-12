using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenJailedState : State

{
    private readonly GreenPlayer greenPlayer;

    public GreenJailedState(GreenPlayer target)
    {
        this.greenPlayer = target;
    }

    public override State Execute()
    {
        greenPlayer.GreenIsJailed();
        return this;
    }

}
