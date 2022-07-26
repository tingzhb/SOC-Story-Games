using System.Threading.Tasks;
using UnityEngine;

public class CorrectCommand : ICommand {

	public Task ExecuteAsync(){
		SendCorrectMessage();
		return Task.CompletedTask;
	}
	public void Undo(){
		throw new System.NotImplementedException();
	}
	private void SendCorrectMessage(){
		CorrectMessage correctMessage = new();
		Broker.InvokeSubscribers(typeof(CorrectMessage), correctMessage);
	}
}
