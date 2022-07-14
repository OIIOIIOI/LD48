using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public static class ExpeditionEventDealer
{

    private static ExpeditionEvents allEvents;
    private static int currentScriptedEvent;

    public static void Init()
    {
        var jsonTextFile = Resources.Load<TextAsset>("events");
        allEvents = JsonUtility.FromJson<ExpeditionEvents>(jsonTextFile.text);

        currentScriptedEvent = 0;
    }

    public static ExpeditionEvent GetEvent(float luck)
    {
        ExpeditionEvent e;
        if (currentScriptedEvent < allEvents.scriptedEvents.Length)
        {
            e = allEvents.scriptedEvents[currentScriptedEvent];
            currentScriptedEvent++;
        }
        else
            e = GetRandomEvent();

        e.CalculateRewards(luck);
        return e;
    }
    
    public static ExpeditionEvent GetRandomEvent()
    {
        return allEvents.randomEvents[Random.Range(0, allEvents.randomEvents.Count())];
    }
    
    /*private static ExpeditionEvent GetRandomTypedEvent(ExpeditionEvent.EventType eventType)
    {
        var events = allEvents.randomEvents.Where(e => (ExpeditionEvent.EventType) Enum.Parse(typeof(ExpeditionEvent.EventType), e.type) == eventType).ToArray();
        return events[Random.Range(0, events.Count())];
    }*/
    
}

[Serializable]
public class ExpeditionEvent
{
    public enum EventType { Loot, Meet, Fight };
    public enum RewardType { People, Scraps, Relic };

    public string type;
    public string text;
    public string[] rewardTypes;
    Dictionary<RewardType, int> rewards;

    public void CalculateRewards(float luck)
    {
        rewards = new Dictionary<RewardType, int>();

        foreach (string rewardType in rewardTypes)
        {
            RewardType rt = (RewardType) Enum.Parse(typeof(RewardType), rewardType);
            int amount = 1;
            switch (rt)
            {
                case RewardType.People:
                    if (luck > 0)       amount = Random.Range(3, 6);
                    else if (luck == 0) amount = Random.Range(1, 3);
                    else                amount = 1;
                    break;
                
                case RewardType.Relic:
                    if (luck > 0)       amount = Random.Range(3, 6);
                    else if (luck == 0) amount = Random.Range(1, 3);
                    else                amount = 1;
                    break;
                
                case RewardType.Scraps:
                    if (luck > 0)       amount = Random.Range(3, 6);
                    else if (luck == 0) amount = Random.Range(1, 3);
                    else                amount = 1;
                    break;
            }
            rewards.Add(rt, amount);
        }
    }
}

[Serializable]
public class ExpeditionEvents
{
    public ExpeditionEvent[] scriptedEvents;
    public ExpeditionEvent[] randomEvents;
}
