using System.Threading.Tasks;

public class ValidAnswerCommand : ICommand {

	public Task ExecuteAsync(){
		SendSuccessMessage();
		return Task.CompletedTask;
	}
	public void Undo(){
		throw new System.NotImplementedException();
	}
	private void SendSuccessMessage(){
		SuccessMessage successMessage = new() {};
		Broker.InvokeSubscribers(typeof(SuccessMessage), successMessage);
	}
}
