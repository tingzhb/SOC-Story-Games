using System.Threading.Tasks;
using UnityEngine;

public class BubbleCommand : ICommand {

	public Task ExecuteAsync(){
		SendBackMessage();
		return Task.CompletedTask;
	}
	public void Undo(){
		throw new System.NotImplementedException();
	}
	private void SendBackMessage(){
		BubbleMessage bubbleMessage = new();
		Broker.InvokeSubscribers(typeof(BubbleMessage), bubbleMessage);
	}
}
