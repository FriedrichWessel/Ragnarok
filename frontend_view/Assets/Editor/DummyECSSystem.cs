using Framework.Systems.ECS;
using System;
using System.Collections.Generic;
using UnityEngine;

public class DummyECSSystem : IECS
{
	public event Action<IComponent> ComponentRegistered = (registeredComponent) => { };
	public event Action<IComponent> ComponentDeRegistered = (deRegisteredComponent) => { };
	public Dictionary<Type, List<IComponent>> RegisteredComponents = new Dictionary<Type, List<IComponent>>();
	public Dictionary<GameObject, List<IComponent>> RegisteredEntities = new Dictionary<GameObject, List<IComponent>>();
	public Dictionary<GameObject, IComponent> ComponentOfType = new Dictionary<GameObject, IComponent>();

	public List<IComponent> AllRegisteredComponents = new List<IComponent>(); 

	public IECSSystem DequeueLastSystem()
	{
		return null;
	}

	public void DeRegisterComponent(IComponent component)
	{
		ComponentDeRegistered(component); 
	}

	public void DeRegisterSystem(IECSSystem system)
	{
		
	}

	public void EnqueueFirstSystem(IECSSystem system)
	{

	}

	public List<T> GetComponentsOfType<T>()
	{
		var result = new List<T>();
		foreach (var comp in RegisteredComponents[typeof(T)]) {
			result.Add((T)comp); 
		}
		return result; 
	}

	public bool HasRegisteredSystem(IECSSystem system)
	{
		return false;
	}

	public void RegisterComponent(IComponent component)
	{
		ComponentRegistered(component); 
	}

	public void Tick()
	{

	}

	public List<IComponent> GetComponentsOfEntity(GameObject entity)
	{
		return this.RegisteredEntities[entity]; 
	}

	public T GetComponentOfEntityWithType<T>(GameObject testEntity)  where T : class, IComponent
	{
		return this.ComponentOfType[testEntity] as T; 
	}

	public List<IComponent> GetAllRegisteredComponents()
	{
		return this.AllRegisteredComponents; 
	}
}