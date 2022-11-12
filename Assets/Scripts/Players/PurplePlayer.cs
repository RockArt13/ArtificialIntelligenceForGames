using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Purple players chase green players
public class PurplePlayer : Player
{
    StateMachine stateMachine;

    // Start overrides the baseclass Start, but uses it.
    protected override void Start()
    {
        base.Start();
        stateMachine = new StateMachine(new PurpleIdleState(this));
    }

    // Update decides what to do, chase greens or bring them to gaol
    protected override void Update()
    {
        stateMachine.Execute();
        // Use the Move method of the parent class
        base.Update();
    }

    public bool Chase(GreenPlayer target)
    {
        Move(target.gameObject, false);
        return target.Captured(gameObject);
    }

    // Find the nearest green player to a given purple player
    public bool FindClosestGreenPlayer()
    {
        GameObject greenTarget = null;
        float closestDistance = float.MaxValue;

        foreach (GameObject greenPlayer in ClosestObjects())
        {
            if (greenPlayer.CompareTag(gameManager.greenTag))
            {
                float distance = Vector2.Distance(greenPlayer.transform.position, transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    greenTarget = greenPlayer;
                }
            }
        }
        target = greenTarget;

        return target;
    }
}
