#if UNITY_EDITOR
using System.Collections;
using System.Net;
using System.Net.Sockets;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class WebsocketIntegrationTest  {

	[UnityTest]
	public IEnumerator SendToLocalNetwork()
	{
		string echoServer = "ws://192.168.2.116:8080/echo";
		yield return SendToEchoAndCheckResult(echoServer);
	}

	private IEnumerator SendToEchoAndCheckResult(string echoServer)
	{
		var tcpClient = new TcpClient("192.168.2.116", 8080);
		var tcpConnection = new TcpConnectionClient(tcpClient);
		tcpConnection.SendData("Test\n", ar =>
		{
			Debug.Log("Test2");
		});

		yield return new WaitForSeconds(1);
		Assert.AreEqual("Test", tcpConnection.GetData());
	}

	

}
#endif