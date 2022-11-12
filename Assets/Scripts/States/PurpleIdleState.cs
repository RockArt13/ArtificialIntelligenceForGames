using System.Collections;
using System.Collections.Generic;

public class PurpleIdleState : State
{
    private readonly PurplePlayer purplePlayer;

  public PurpleIdleState(PurplePlayer chaser)
    {
        this.purplePlayer = chaser;
    }

  public override State Execute()
  {
  if(purplePlayer.FindClosestGreenPlayer())
        {
            return new PurpleChasingState(purplePlayer, purplePlayer.GetTarget().GetComponent<GreenPlayer>());
        }
    else
        {
            return this;
        }
  }
}
