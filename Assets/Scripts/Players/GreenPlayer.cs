using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenPlayer : Player
{

    public bool captured;


    StateMachine stateMachine;

    protected override void Start()
    {
        base.Start();
        stateMachine = new StateMachine(new GreenIdleState(this));
    }

    protected override void Update()
    {
        base.Update();

        stateMachine.Execute();
    }

    public bool Captured(GameObject purple)
    {
        float distance = Vector2.Distance(transform.position, purple.transform.position);
        if (distance <= 3f) // catch distance
        {
            capturer = purple;
            return captured = true;
        }
        else
            return false;
    }


    public void GreenIsJailed()
    {
        capturer.GetComponent<PurplePlayer>().SetTarget(null);
        capturer = null;
        gameObject.SetActive(false);
        gameManager.RemoveGreenPlayer();
    }

    public bool IsJailed()
    {
        return Vector2.Distance(transform.position, new Vector3(gameManager.goal.transform.position.x, 0f, gameManager.goal.transform.position.z)) <= 4f; //jailed distance
    }
}
