using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework.Systems.Tests {

	public class ComponentBaseTest  {

		private DummyECSSystem TestECS;
		private ComponentBase TestComponent;
		private GameObject TestEntity; 

		[SetUp]
		public void RunBeforeEveryTest() {
			this.TestEntity = new GameObject();
			this.TestECS = new DummyECSSystem(); 
			this.TestComponent = TestEntity.AddComponent<ComponentBase>();
			this.TestComponent.System = TestECS; 
			
		}

		[TearDown]
		public void RunAfterEveryTest()
		{
			if (this.TestComponent != null && this.TestComponent.gameObject != null) {
				GameObject.DestroyImmediate(this.TestComponent.gameObject);
			}
		}

		[Test]
		public void RegistersComponentOnInit()
		{
			bool called = false;
			this.TestECS.ComponentRegistered += (c) => { called = c.Equals(this.TestComponent); }; 
			this.TestComponent.Init();
			Assert.True(called);
		}

		[Test]
		public void DeRegisterOnDestroy()
		{
			bool called = false;
			this.TestECS.ComponentDeRegistered += (c) => { called = c.Equals(this.TestComponent); };
			TestComponent.OnDestroy(); 
			Assert.True(called);
		}

		[Test]
		public void EntitIsGameObject() {
			Assert.AreEqual(this.TestComponent.Entity, this.TestEntity.gameObject); 
		}
	}
}
