using NUnit.Framework;
using UnityEngine;

namespace Components.Tests
{
	public class MoveComponentTest
	{
	
		private MovementComponent _testComponent;
		private ITimeProvider _timeProvider;
		
		[SetUp]
		public void RunBeforeEveryTest()
		{
			_testComponent = new GameObject().AddComponent<MovementComponent>();
		}
	
		[TearDown]
		public void RunAfterEveryTest()
		{
			GameObject.DestroyImmediate(_testComponent);
		}
	
		[Test]
		public void EvaluateShouldUpdateObjectTransform()
		{
			_testComponent.Position = new Vector3(1.5f,0,0);
			_testComponent.Evaluate();
			Assert.AreEqual(_testComponent.Entity.transform.position, new Vector3(1.5f,0,0));
		}
	}
}
