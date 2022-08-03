using System.Threading.Tasks;
using UnityEngine;

public class PlayVOCommand : ICommand {

	public Task ExecuteAsync(){
		PlayVO();
		return Task.CompletedTask;
	}
	public void Undo(){
		throw new System.NotImplementedException();
	}
	private void PlayVO(){
		SoundMessage soundMessage = new(){
			SoundType = 98
		};
		Broker.InvokeSubscribers(typeof(SoundMessage), soundMessage);
	}
}