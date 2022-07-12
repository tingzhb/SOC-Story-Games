using System.Threading.Tasks;
using UnityEngine;

public class StickRCommand : ICommand {

	public Task ExecuteAsync(){
		SendStickRMessage();
		return Task.CompletedTask;
	}
	public void Undo(){
		throw new System.NotImplementedException();
	}
	private void SendStickRMessage(){
		StickMessage stickMessage = new(){
			IsLeft = false
		};
		Broker.InvokeSubscribers(typeof(StickMessage), stickMessage);
	}
}
