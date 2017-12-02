using UnityEngine;

public class MovementComponent : ComponentBase
{

	public Vector3 Velocity { get; set; }
	public Vector3 Position { get; set; }

	public void Evaluate()
	{
		this.Entity.transform.position = Position;
	}

}
