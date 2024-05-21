using UnityEngine;

public class SendGlobalEventAction : ActionBase
{
    [SerializeField] private EventNames eventName;
    public object Data;
    public override void Execute()
    {
        GlobalEvents.CallEvent(eventName, Data);
    }
}