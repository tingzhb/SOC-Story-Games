using System.Threading.Tasks;
using UnityEngine;

public class InvalidAnswerCommand : ICommand {

	public Task ExecuteAsync(){
		SendFailureMessage();
		return Task.CompletedTask;
	}
	public void Undo(){
		throw new System.NotImplementedException();
	}
	private void SendFailureMessage(){
		InvalidMessage invalidMessage = new(){ };
		Broker.InvokeSubscribers(typeof(InvalidMessage), invalidMessage);
	}
}