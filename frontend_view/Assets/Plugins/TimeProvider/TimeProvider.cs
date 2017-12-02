using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TimeProvider : ITimeProvider, ITickable
{
	public float GameDeltaTime
	{
		get; private set; 
	}

	public void Tick()
	{
		this.GameDeltaTime = Time.deltaTime; 
	}
}
