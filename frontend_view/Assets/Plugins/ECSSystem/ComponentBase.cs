using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ComponentBase : MonoBehaviour, IComponent{

	[SerializeField]
	private bool _stayOnDeath; 
	public bool StayOnDeath { get { return _stayOnDeath;  }
		set {
			_stayOnDeath = value; 
		}
	}

	[Inject]
	public IECS System;

	private GameObject _cachedEntity; 
	public GameObject Entity
	{
		get
		{
			if (_cachedEntity == null) {
				_cachedEntity = gameObject; 
			}
			return _cachedEntity;
		}
	}

	public bool IsDead { get; private set; }

	public float TimeAlive{ get; set; }

	[Inject]
	public virtual void Init() {
		System.RegisterComponent(this);
	}

	public void OnDestroy()
	{
		Cleanup();
	}

	public void Cleanup()
	{
		System.DeRegisterComponent(this);
	}

	public virtual void MarkDead()
	{
		this.IsDead = true; 
	}
}
