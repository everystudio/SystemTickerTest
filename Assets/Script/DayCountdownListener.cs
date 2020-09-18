using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class UnityEventDate : UnityEvent<System.DateTime> { }

public class DayCountdownListener : ScriptableEventListener<System.DateTime>
{
    [SerializeField]
    private TimeEvent eventObject;

    private UnityEventDate eventAction = new UnityEventDate();

    protected override ScriptableEvent<DateTime> ScriptableEvent
    {
        get
        {
            return eventObject;
        }
    }
    protected override UnityEvent<DateTime> Action
    {
        get
        {
            return eventAction;
        }
    }

    private DateTime? lastSeen = null;

    [SerializeField]
    private int totalAmount;

    [SerializeField]
    public UnityEvent onCountEvent;

    [System.Serializable]
    private class DayCountEvents
    {
        [Tooltip("At what count (x: min, y: max) should this event be fired?")]
        public Vector2Int count;
        public UnityEvent onCountEvent;
    }

    [SerializeField]
    private DayCountEvents[] dayCountEvents;

    private int currentAmount;

    private void Awake()
    {
        eventAction?.AddListener(OnTick);
    }

    private void OnDestroy()
    {
        eventAction?.RemoveListener(OnTick);
    }

    private void OnTick(DateTime dateTime)
    {
        dateTime = new DateTime(
            dateTime.Year,
            dateTime.Month,
            dateTime.Day,
            dateTime.Hour,
            dateTime.Minute,
            dateTime.Second);


        if (lastSeen == null)
        {
            lastSeen = dateTime;
            currentAmount = totalAmount;
        }

        currentAmount -= (int)(dateTime - (DateTime)lastSeen).TotalMinutes;
        lastSeen = dateTime;

        for (int i = 0; i < dayCountEvents.Length; i++)
        {
            if (currentAmount >= dayCountEvents[i].count.x && currentAmount <= dayCountEvents[i].count.y)
            {
                dayCountEvents[i].onCountEvent.Invoke();
            }
        }


    }



}
