using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using EditorConnectionWindow.BaseSystem;
using UnityEngine;

public class TcpConnectionClient : IConnectionClient
{
	private TcpClient _tcpClient;
 
	public string IpAddress { get; private set; }
	public bool HasData {
		get { return _tcpClient.GetStream().DataAvailable; }
	}
	
	public TcpConnectionClient(TcpClient clientSocket) 
	{
		_tcpClient = clientSocket;
		var adress = ((IPEndPoint)clientSocket.Client.RemoteEndPoint).Address.ToString();
		IpAddress = adress;
	}

	public void SendData(string data, AsyncCallback ar)
	{
		var stream = _tcpClient.GetStream(); 
		var writer = new StreamWriter(stream);
		byte[] bytes = Encoding.ASCII.GetBytes(data);
		stream.BeginWrite(bytes, 0, bytes.Length, ar, new object());
	}

	public string GetData()
	{
		string result = string.Empty;
		if (HasData)
		{
			var reader = new StreamReader(_tcpClient.GetStream(), true);
			result = reader.ReadLine();
		}
		return result;
	}
}