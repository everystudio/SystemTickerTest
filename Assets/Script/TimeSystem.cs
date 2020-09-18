using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSystem : GameSystem
{
	[SerializeField]
	private TimeEvent timeEvent_Day;
	[SerializeField]
	private TimeEvent timeEvent_Minute;
	[SerializeField]
	private TimeEvent timeEvent_Second;

	[SerializeField, Range(1, 9999)]
	private int year = 2020;

	[SerializeField, Range(1, 12)]
	private int month = 9;

	[SerializeField, Range(1, 31)]
	private int day = 18;

	[SerializeField, Range(0, 23)]
	private int hour = 13;

	public DateTime startTime;
	public DateTime currentTime;
	public TimeSpan ElapsedTime => currentTime - startTime;


	public override void OnLoadSystem()
	{
		startTime = new DateTime(year, month, day, hour, 0, 0);
		currentTime = startTime;

		//pauzeEvent?.AddListener(OnGamePauzed);
	}

	public void Start()
	{
		timeEvent_Day?.Invoke(currentTime);
		timeEvent_Minute?.Invoke(currentTime);
		timeEvent_Second?.Invoke(currentTime);
	}
	public override void OnTick()
	{
		DateTime currentTimeTemp = currentTime;
		AddSecond(1);

		if(currentTimeTemp.Minute != currentTime.Minute)
		{
			timeEvent_Minute.Invoke(currentTime);
			//Debug.Log("add minute");
		}
		if ( currentTimeTemp.DayOfWeek != currentTime.DayOfWeek)
		{
			timeEvent_Day.Invoke(currentTime);
			//Debug.Log("add day");
		}

	}

	public void AddSecond(int amount)
	{
		currentTime = currentTime.AddSeconds(amount);
		timeEvent_Second?.Invoke(currentTime);
		Debug.Log("add second");
	}


}
