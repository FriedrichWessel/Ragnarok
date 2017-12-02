using Framework.Systems.ECS;
using System;
using WhiteKnight.Systems;
using Zenject;

public class SystemFactory {
	private DiContainer Container;

	public SystemFactory(DiContainer container) {
		this.Container = container; 
	}

	public IECSSystem Create<T>() where T : IECSSystem {
		return Container.Instantiate<T>(); 
		
	}
}
