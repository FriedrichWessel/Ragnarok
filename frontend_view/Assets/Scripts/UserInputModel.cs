using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInputModel
{
	public UserInputType type;
}

public enum UserInputType
{
	Run = 0
}

public class GameStateModel
{
	// microseconds UTC
	public long timestamp; 
	// 0 = bottom of screen
	public float groundHeight;
	public WorldObject[] worldObjects; 
}

public class WorldObject
{
	public string uuid;
	public string modelId;
	public float[] position;
	public List<ComponentData> components;
}

public enum ComponentType
{
	Movement = 0, 
	Exhaust = 1
}

public class ComponentData
{
	public ComponentType Type; 
}

public class MovementComponentData : ComponentData()
{
	public MovementComponentData()
	{
		Type = ComponentType.Movement;
	}

	public float[] Velocity;
}


