using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Framework.Systems.ECS {

	public class ECSSystem : ITickable, IECS
	{
		private List<IECSSystem> RegisteredSystems = new List<IECSSystem>();
		private Dictionary<Type, List<IComponent>> RegisteredComponents = new Dictionary<Type, List<IComponent>>();
		private Dictionary<GameObject, List<IComponent>> RegisteredEntities = new Dictionary<GameObject, List<IComponent>>(); 

		public void Tick()
		{
			foreach (var system in RegisteredSystems) {
				system.Tick(); 
			}	
		}

		public void EnqueueFirstSystem(IECSSystem system)
		{
			this.RegisteredSystems.Add(system); 
		}

		public IECSSystem DequeueLastSystem() {
			var dequeuedSystem = this.RegisteredSystems[this.RegisteredSystems.Count - 1]; 
			this.RegisteredSystems.RemoveAt(this.RegisteredSystems.Count - 1);
			return dequeuedSystem; 
		}
		public void DeRegisterSystem(IECSSystem system) {
			this.RegisteredSystems.Remove(system); 
		}

		public bool HasRegisteredSystem(IECSSystem system)
		{
			return this.RegisteredSystems.Contains(system); 
		}

		public void RegisterComponent(IComponent component) {
			if (!this.RegisteredComponents.ContainsKey(component.GetType())) {
				this.RegisteredComponents.Add(component.GetType(), new List<IComponent>());
			}
			if (!this.RegisteredEntities.ContainsKey(component.Entity)) {
				this.RegisteredEntities.Add(component.Entity, new List<IComponent>());
			}
			this.RegisteredComponents[component.GetType()].Add(component);
			this.RegisteredEntities[component.Entity].Add(component); 
		}

		public void DeRegisterComponent(IComponent component) {
			GetComponentsOfType(component.GetType()).Remove(component);
			GetComponentsOfEntity(component.Entity).Remove(component);
		}

		public List<T> GetComponentsOfType<T>() {
			var result = new List<T>(); 
			if (this.RegisteredComponents.ContainsKey(typeof(T))) {
				var l = this.RegisteredComponents[typeof(T)];
				foreach(var comp in l) {
					result.Add((T)comp); 
				}
			}
			return result; 
		}

		private List<IComponent> GetComponentsOfType(Type t) {
			if (this.RegisteredComponents.ContainsKey(t)){
				return this.RegisteredComponents[t]; 
			}
			return new List<IComponent>(); 
		}

		public List<IComponent> GetComponentsOfEntity(GameObject entity)
		{
			if (this.RegisteredEntities.ContainsKey(entity)) {
				return this.RegisteredEntities[entity]; 
			}
			return new List<IComponent>(); 
		}

		public T GetComponentOfEntityWithType<T>(GameObject testEntity)  where T : class, IComponent
		{
			T result = null; 
			var comps = GetComponentsOfEntity(testEntity);
			foreach (var comp in comps) {
				result = comp as T;
				if (result != null) {
					return result; 
				}
			}
			return result;
		}

		public List<IComponent> GetAllRegisteredComponents()
		{
			var result = new List<IComponent>(); 
			foreach (var kv in RegisteredComponents) {
				result.AddRange(kv.Value); 
			}
			return result; 
		}
	}
}
