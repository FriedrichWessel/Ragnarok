﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITimeProvider  {
	float GameDeltaTime { get; }
	void Tick(); 
}
