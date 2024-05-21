using System.Collections.Generic;
using UnityEngine;

public class ActionExecutorBase : MonoBehaviour
{
    [SerializeField] private List<ActionBase> actions;
    [SerializeField] private ActionConditionBase condition;
    [SerializeField] private bool executeOnAwake;

    private void Awake()
    {
        if (executeOnAwake) Execute();
    }

    public void Execute()
    {
        if (condition == null || condition.Check()) ExecuteAll();
    }

    private void ExecuteAll()
    {
        actions.ForEach(action => action.Execute());
    }
}