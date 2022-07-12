using System.Threading.Tasks;
using UnityEngine;

public class StickLCommand : ICommand {

	public Task ExecuteAsync(){
		SendStickLMessage();
		return Task.CompletedTask;
	}
	public void Undo(){
		throw new System.NotImplementedException();
	}
	private void SendStickLMessage(){
		StickMessage stickMessage = new(){
			IsLeft = true
		};
		Broker.InvokeSubscribers(typeof(StickMessage), stickMessage);
	}
}
