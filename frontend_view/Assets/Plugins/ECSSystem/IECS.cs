using Framework.Systems.ECS;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IECS  {

	void Tick();

	void EnqueueFirstSystem(IECSSystem system);
	IECSSystem DequeueLastSystem();
	void DeRegisterSystem(IECSSystem system);
	bool HasRegisteredSystem(IECSSystem system);
	void RegisterComponent(IComponent component);
	void DeRegisterComponent(IComponent component);
	List<T> GetComponentsOfType<T>();
	List<IComponent> GetComponentsOfEntity(GameObject entity);
	T GetComponentOfEntityWithType<T>(GameObject testEntity) where T : class, IComponent;
	List<IComponent> GetAllRegisteredComponents(); 
}
