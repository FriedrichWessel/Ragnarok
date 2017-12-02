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
	public List<Player> players;
	// 0 = bottom of screen
	public float groundHeight;
	public WorldObject[] worldObjects; 
}
/*
public class UpdateGameStateModel
{
	public List<Player> addPlayers; 
	// list of uuid
	public List<string> removePlayer;
	public List<Player> updatePlayer;
}*/

public class WorldObject
{
	public string uuid;
	public string modelId;
	public float[] position;
}

public class Player
{
	public string uuid;
	public float[] position;
	public string modelId;
	public float[] velocity;
}


