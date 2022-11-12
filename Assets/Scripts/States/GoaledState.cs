/* 
 * using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoaledState : State
{
    GreenPlayer target;
    PurplePlayer player;

    public GoaledState(PurplePlayer player, GreenPlayer target)
    {
        this.player = player;
        this.target = target;
    }

    public override State Execute()
    {

        if (GameManager.Instance().GreenPlayerCaptured())
        {

            return new PurpleIdleState(player);

        }
        return this;
    }


}
*/
