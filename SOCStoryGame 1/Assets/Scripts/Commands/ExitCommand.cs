using System.Threading.Tasks;
using UnityEngine;

public class ExitCommand : ICommand {

	public Task ExecuteAsync(){
		SendFailureMessage();
		return Task.CompletedTask;
	}
	public void Undo(){
		throw new System.NotImplementedException();
	}
	private void SendFailureMessage(){
		ExitMessage exitMessage = new(){ };
		Broker.InvokeSubscribers(typeof(ExitMessage), exitMessage);
	}
}