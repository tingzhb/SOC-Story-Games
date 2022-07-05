using UnityEngine;

public class SingleTapController : MonoBehaviour {

	private Executor executor;
	private void Awake() {
		executor = FindObjectOfType<Executor>();
		Broker.Subscribe<SingleTapMessage>(OnSingleTapMessageReceived);
	}
	private void OnSingleTapMessageReceived(SingleTapMessage obj) {
		ValidateTap(obj.TappedObject);
	}
	private void ValidateTap(string tappedObject){
		switch (tappedObject){
			// Consider Sending String
			case "Valid":
				executor.Enqueue(new ValidAnswerCommand());
				break;
			case "Invalid":
				executor.Enqueue(new InvalidAnswerCommand());
				break;
			case "Failure":
				executor.Enqueue(new FailureCommand());
				break;
			case "Exit":
				executor.Enqueue(new ExitCommand());
				break;
			case "Back":
				executor.Enqueue(new BackCommand());
				break;
			case "Sound":
				executor.Enqueue(new PlaySoundCommand());
				break;
		}
	}
}
