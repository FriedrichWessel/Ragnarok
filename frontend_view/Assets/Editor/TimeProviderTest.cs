using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class TimeProviderTest {

	private TimeProvider TestProvider;

	[SetUp]
	public void RunBeforeEveryTest() {
		this.TestProvider = new TimeProvider(); 
	}

	[Test]
	public void TickSetsGameTimeToUnityDeltaTime() {
		this.TestProvider.Tick();
		Assert.AreEqual(this.TestProvider.GameDeltaTime, Time.deltaTime); 
	}

	
}
