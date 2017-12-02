using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

public class DummyStayOnDeathComp : IComponent
{
	public DummyStayOnDeathComp(GameObject entity) { this.Entity = entity; }
	public GameObject Entity
	{
		get; set;
	}

	public bool IsDead
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	public bool StayOnDeath
	{
		get
		{
			return true;
		}
	}

	public float TimeAlive
	{
		get; set;

	}

	public void Cleanup()
	{

	}

	public void MarkDead()
	{
		throw new NotImplementedException();
	}
}

public class DummyDieOnDeathComp : IComponent
{
	public DummyDieOnDeathComp(GameObject entity) { this.Entity = entity; }
	public GameObject Entity
	{
		get; set;
	}

	public bool IsDead
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	public bool StayOnDeath
	{
		get
		{
			return false;
		}
	}

	public float TimeAlive
	{
		get; set;
	}

	public void Cleanup()
	{

	}

	public void MarkDead()
	{
		throw new NotImplementedException();
	}
}
