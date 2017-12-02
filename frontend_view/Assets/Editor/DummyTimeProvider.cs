public class DummyTimeProvider : ITimeProvider
{
	public float GameDeltaTime
	{
		get; set;
	}

	public void Tick() { }
}