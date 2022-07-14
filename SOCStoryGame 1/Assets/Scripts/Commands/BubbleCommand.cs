using System.Threading.Tasks;
using UnityEngine;

public class BubbleCommand : ICommand {

	public Task ExecuteAsync(){
		SendBubbleMessage();
		return Task.CompletedTask;
	}
	public void Undo(){
		throw new System.NotImplementedException();
	}
	private void SendBubbleMessage(){
		CorrectMessage correctMessage = new();
		Broker.InvokeSubscribers(typeof(CorrectMessage), correctMessage);
	}
}
