using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IComponent  {

	bool IsDead { get;  }
	bool StayOnDeath { get;  } 
	GameObject Entity { get;}
	float TimeAlive { get; set; }

	void Cleanup();
	void MarkDead(); 
	
}
