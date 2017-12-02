using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework.Systems.ECS {
	public interface IECSSystem  {
		void Tick();
	}
}
