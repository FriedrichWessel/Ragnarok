using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
namespace WhiteKnight.Systems {

	public abstract class BaseSystem<T> : ITickable {

		protected HashSet<T> RegisteredComponents = new HashSet<T>();
		public int ComponentCount
		{
			get
			{
				return this.RegisteredComponents.Count;
			}
		}

		public void DeRegisterComponent(T component)
		{
			this.RegisteredComponents.Remove(component);
		}

		public void RegisterComponent(T component)
		{
			this.RegisteredComponents.Add(component);
		}

		public abstract void Tick(); 
	}
}
