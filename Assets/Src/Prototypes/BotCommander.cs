using Src.PVE.BotTriggers;
using UnityEngine;

public class BotCommander : MonoBehaviour
{
    private ManualDelayedBotMoveTrigger _manualDelayedBotMoveTrigger;
    
    public void Init(ManualDelayedBotMoveTrigger manualDelayedBotMoveTrigger)
    {
        _manualDelayedBotMoveTrigger = manualDelayedBotMoveTrigger;
    }
    
    public void CommandBotToMove()
    {
        _manualDelayedBotMoveTrigger.TriggerBot();
    }
}
