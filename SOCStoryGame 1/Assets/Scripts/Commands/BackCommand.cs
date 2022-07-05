using System.Threading.Tasks;
using UnityEngine;

public class BackCommand : ICommand {

	public Task ExecuteAsync(){
		SendBackMessage();
		return Task.CompletedTask;
	}
	public void Undo(){
		throw new System.NotImplementedException();
	}
	private void SendBackMessage(){
		BackMessage backMessage = new();
		Broker.InvokeSubscribers(typeof(BackMessage), backMessage);
	}
}
