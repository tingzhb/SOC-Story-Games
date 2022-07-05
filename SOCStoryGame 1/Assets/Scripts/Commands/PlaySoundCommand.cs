using System.Threading.Tasks;
using UnityEngine;

public class PlaySoundCommand : ICommand {

	public Task ExecuteAsync(){
		PlaySound();
		return Task.CompletedTask;
	}
	public void Undo(){
		throw new System.NotImplementedException();
	}
	private void PlaySound(){
		SoundMessage soundMessage = new(){
			SoundType = 2
		};
		Broker.InvokeSubscribers(typeof(SoundMessage), soundMessage);
	}
}