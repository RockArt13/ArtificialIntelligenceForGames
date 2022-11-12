using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Goal
{
  public const string StayFree = "StayFree";
  public const string StayRested = "StayRested";

  public string name;

  public float priority;

  protected virtual void UpdatePriority(float increment)
  {
    priority += increment;
  }

}
