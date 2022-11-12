using System.Collections;
using System.Collections.Generic;


public class PurpleChasingState : State
{
  private readonly GreenPlayer greenPlayer = null;
  private readonly PurplePlayer purplePlayer;

  public PurpleChasingState(PurplePlayer chaser, GreenPlayer target)
  {
    this.purplePlayer = chaser;
    this.greenPlayer = target;
  }

    public override State Execute()
    {
        if (purplePlayer.Chase(greenPlayer))
        { 
            return new PurpleEscortingState(purplePlayer, greenPlayer);
        }
        else
            return this;
    }
}
