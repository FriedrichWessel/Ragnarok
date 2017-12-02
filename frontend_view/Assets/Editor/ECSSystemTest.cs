using NUnit.Framework;
using Framework.Systems.ECS;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace Framework.Systems.Tests {

	public class ECSSystemTest {

		private ECSSystem TestSystem;
		private DummyTimeProvider TimeProvider; 

		private GameObject TestEntity;
		private GameObject TestEntity2;

		[SetUp]
		public void RunBeforeEveryTest() {
			this.TimeProvider = new DummyTimeProvider(); 
			this.TestSystem = new ECSSystem();
			this.TestEntity = new GameObject();
			this.TestEntity2 = new GameObject();
		}

		[TearDown]
		public void RunAfterEveryTest() {
			
			GameObject.DestroyImmediate(this.TestEntity);
			GameObject.DestroyImmediate(this.TestEntity2);

		}

		

		[Test]
		public void CanEnqueueSystem() {
			var dummySystem = new DummySystem();
			this.TestSystem.EnqueueFirstSystem(dummySystem);
			Assert.IsTrue(this.TestSystem.HasRegisteredSystem(dummySystem));
		}

		[Test]
		public void CanDeRegisterSystem() {
			var dummySystem = new DummySystem();
			this.TestSystem.EnqueueFirstSystem(dummySystem);
			this.TestSystem.DeRegisterSystem(dummySystem);
			Assert.False(this.TestSystem.HasRegisteredSystem(dummySystem));
		}

		[Test]
		public void DeRegisterOnlyRemovesTheSpecificSystem() {
			var dummySystem1 = new DummySystem();
			var dummySystem2 = new DummySystem();
			var dummySystem3 = new DummySystem();
			this.TestSystem.EnqueueFirstSystem(dummySystem1);
			this.TestSystem.EnqueueFirstSystem(dummySystem2);
			this.TestSystem.EnqueueFirstSystem(dummySystem3);
			this.TestSystem.DeRegisterSystem(dummySystem2);
			Assert.IsTrue(this.TestSystem.HasRegisteredSystem(dummySystem3));
			Assert.IsTrue(this.TestSystem.HasRegisteredSystem(dummySystem1));
		}

		[Test]
		public void SystemsStayInOrderAfterRemoveOne() {
			var dummySystem1 = new DummySystem(1);
			var dummySystem2 = new DummySystem(2);
			var dummySystem3 = new DummySystem(3);
			var dummySystem4 = new DummySystem(4);
			this.TestSystem.EnqueueFirstSystem(dummySystem1);
			this.TestSystem.EnqueueFirstSystem(dummySystem2);
			this.TestSystem.EnqueueFirstSystem(dummySystem3);
			this.TestSystem.EnqueueFirstSystem(dummySystem4);
			this.TestSystem.DeRegisterSystem(dummySystem3);
			Assert.AreEqual(dummySystem4, this.TestSystem.DequeueLastSystem());
			Assert.AreEqual(dummySystem2, this.TestSystem.DequeueLastSystem());
			Assert.AreEqual(dummySystem1, this.TestSystem.DequeueLastSystem());

		}
		[Test]
		public void EnqueuedSystemsGetTicked() {
			var dummySystem = new DummySystem();
			this.TestSystem.EnqueueFirstSystem(dummySystem);
			this.TestSystem.Tick();
			Assert.AreEqual(1, dummySystem.TickCount);
		}

		[Test]
		public void RemovedSystemsDontGetTicked() {
			var dummySystem1 = new DummySystem();
			var dummySystem2 = new DummySystem();
			this.TestSystem.EnqueueFirstSystem(dummySystem1);
			this.TestSystem.EnqueueFirstSystem(dummySystem2);
			this.TestSystem.Tick();
			this.TestSystem.DeRegisterSystem(dummySystem2);
			this.TestSystem.Tick();
			Assert.AreEqual(2, dummySystem1.TickCount);
			Assert.AreEqual(1, dummySystem2.TickCount);
		}

		[Test]
		public void SystemsGetTickedInOrder() {
			var dummySystem1 = new DummySystem();
			var dummySystem2 = new DummySystem();
			var tickList = new List<DummySystem>();
			this.TestSystem.EnqueueFirstSystem(dummySystem1);
			this.TestSystem.EnqueueFirstSystem(dummySystem2);
			dummySystem1.Ticked += () => { tickList.Add(dummySystem1); };
			dummySystem2.Ticked += () => { tickList.Add(dummySystem2); };
			this.TestSystem.Tick();
			Assert.AreEqual(dummySystem1, tickList[0]);
			Assert.AreEqual(dummySystem2, tickList[1]);

		}

		[Test]
		public void CanRegisterComponent() {
			var component = new DummyStayOnDeathComp(this.TestEntity);
			this.TestSystem.RegisterComponent(component);
			var components = this.TestSystem.GetComponentsOfType<DummyStayOnDeathComp>();
			Assert.Contains(component,  components); 
		}

		[Test]
		public void CanDeRegisterComponent() {
			var component = new DummyStayOnDeathComp(this.TestEntity);
			this.TestSystem.RegisterComponent(component);
			this.TestSystem.DeRegisterComponent(component);
			var components = this.TestSystem.GetComponentsOfType<DummyStayOnDeathComp>();
			Assert.IsFalse(components.Contains(component));
		}
		[Test]
		public void DifferentComponentsStoredInDifferendLists() {
			var component1 = new DummyStayOnDeathComp(this.TestEntity);
			var component2 = new DummyDieOnDeathComp(this.TestEntity);
			this.TestSystem.RegisterComponent(component1);
			this.TestSystem.RegisterComponent(component2);
			var comps1 = this.TestSystem.GetComponentsOfType<DummyStayOnDeathComp>();
			var comps2 = this.TestSystem.GetComponentsOfType<DummyDieOnDeathComp>();
			Assert.Contains(component1, comps1);
			Assert.Contains(component2, comps2);
			Assert.IsTrue(comps1.Contains(component1));
			Assert.IsTrue(comps2.Contains(component2));
			Assert.AreEqual(1, comps1.Count);
			Assert.AreEqual(1, comps2.Count);
		}

		[Test]
		public void CanQueryAllComponentsOnEntity() {
			var component1 = new DummyStayOnDeathComp(this.TestEntity);
			var component2 = new DummyDieOnDeathComp(this.TestEntity);
			var component3 = new DummyStayOnDeathComp(this.TestEntity2); 
			this.TestSystem.RegisterComponent(component1);
			this.TestSystem.RegisterComponent(component2);
			this.TestSystem.RegisterComponent(component3);
			List<IComponent> compList = this.TestSystem.GetComponentsOfEntity(this.TestEntity);
			Assert.Contains(component1, compList);
			Assert.Contains(component2, compList);
			Assert.IsFalse(compList.Contains(component3));
		}

		[Test]
		public void CanQuerySpecialEntityOfSpecialComponent() {
			var component1 = new DummyStayOnDeathComp(this.TestEntity);
			this.TestSystem.RegisterComponent(component1);
			DummyStayOnDeathComp foundComp  = this.TestSystem.GetComponentOfEntityWithType<DummyStayOnDeathComp>(this.TestEntity);
			Assert.AreEqual(foundComp, component1); 
		}

		[Test]
		public void DeRegisterComponentRemovesComponentFrom()
		{
			var component1 = new DummyStayOnDeathComp(this.TestEntity);
			var component2 = new DummyDieOnDeathComp(this.TestEntity);
			this.TestSystem.RegisterComponent(component1);
			this.TestSystem.RegisterComponent(component2);
			this.TestSystem.DeRegisterComponent(component1); 
			List<IComponent> compList = this.TestSystem.GetComponentsOfEntity(this.TestEntity);
			Assert.Contains(component2, compList);
			Assert.IsFalse(compList.Contains(component1));
		}

		[Test]
		public void GetAllComponentsReturnsAllRegisteredComponents() {
			var component1 = new DummyStayOnDeathComp(this.TestEntity);
			var component2 = new DummyDieOnDeathComp(this.TestEntity);
			this.TestSystem.RegisterComponent(component1);
			this.TestSystem.RegisterComponent(component2);
			var list = this.TestSystem.GetAllRegisteredComponents();
			Assert.Contains(component1, list);
			Assert.Contains(component2, list); 

		}
		

		private class DummySystem : IECSSystem{
			public event System.Action Ticked = () => { }; 
			public int TickCount { get; private set;  }
			public int Id { get; set; }
			public DummySystem() { }
			public DummySystem(int id) { this.Id = id;  }
			public void Tick() {
				TickCount++;
				Ticked(); 
			}
		}

		






	}
}
