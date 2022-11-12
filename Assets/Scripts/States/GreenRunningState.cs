using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenRunningState : State
{
    private readonly GreenPlayer greenPlayer;

     public GreenRunningState(GreenPlayer greenPlayer)
     {
         this.greenPlayer = greenPlayer;
     }


    public override State Execute()
    {
        if (greenPlayer.captured)
        {
            return new GreenEscortedState(greenPlayer);
        }
        else
        { 
          return this;
        }
    }
}

