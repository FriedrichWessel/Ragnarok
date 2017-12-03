﻿using System;

namespace EditorConnectionWindow.BaseSystem
{
	public interface IConnectionClient  {

		string IpAddress { get;  }
		bool HasData { get; }
		string GetData();
		void SendData(string data, AsyncCallback aerAsyncCallback);
	}
}