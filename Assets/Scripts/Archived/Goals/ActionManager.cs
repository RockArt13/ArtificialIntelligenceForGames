using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour
{
  List<Action> actions = new List<Action>();
  List<Goal> goals = new List<Goal>();

  // Start is called before the first frame update
  void Start()
  {
    goals.Add(new GreenPlayerBeRested());
    goals.Add(new GreenPlayerStayFree());

    actions.Add(new GreenPlayerRunAwayAction());

  }

  private void Update()
  {
    // Action bestAction = ChooseAction(goals, actions);
    // Action bestAction = ChooseDiscontentedAction(goals, actions);

    Action bestAction = RecursiveChooseAction(out _, goals, actions, 3, 0f);

    bestAction.Execute();

  }

  // Time complexity = O(goals.Length + actions.Length)
  Action ChooseAction(List<Goal> goals, List<Action> actions)
  {
    Goal maximumGoal = goals[0];

    foreach (Goal goal in goals)
      if (goal.priority > maximumGoal.priority)
        maximumGoal = goal;

    Action maximumAction = actions[0];
    foreach (Action action in actions)
      if (action.PriorityChange(maximumGoal) > maximumAction.PriorityChange(maximumGoal))
        maximumAction = action;

    return maximumAction;
  }

  // Time complexity = O(goals.Length * actions.Length)
  Action ChooseDiscontentedAction(List<Goal> goals, List<Action> actions)
  {
    Action maximumAction = null;
    float bestDiscontent = int.MaxValue;

    foreach (Action action in actions)
    {
      float discontent = Discontent(action, goals);
      if (discontent < bestDiscontent)
      {
        maximumAction = action;
        bestDiscontent = discontent;
      }
    }

    return maximumAction;
  }

  float Discontent(Action action, List<Goal> goals)
  {
    float discontent = 0;

    foreach (Goal goal in goals)
    {
      float d = goal.priority + action.PriorityChange(goal);

      discontent += d * d;
    }

    return discontent;
  }

  // Time complexity = O(goals.Length * actions.Length^depth)
  Action RecursiveChooseAction(out float totalDiscontent, List<Goal> goals, List<Action> actions, int depth, float discontent)
  {
    if (depth == 0)
    {
      totalDiscontent = discontent;
      return null;
    }
    else
    {
      float minimumDiscontent = float.MaxValue;
      Action bestAction = null;

      foreach (Action action in actions)
      {
        float disco = Discontent(action, goals);

        // We ignore the action returned
        RecursiveChooseAction(out totalDiscontent, goals, actions, depth - 1, disco + discontent);

        if (totalDiscontent < minimumDiscontent)
        {
          bestAction = action;
          minimumDiscontent = totalDiscontent;
        }

      }
      totalDiscontent = minimumDiscontent;
      return bestAction;

    }
  }
}