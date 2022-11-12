using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action 
{
  protected string name;

  protected float priority;

  public virtual void Execute()
  { }

  public virtual float PriorityChange(Goal g)
  {
    return 0f;
  }
}
