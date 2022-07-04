using UnityEngine;

public class SingleTapController : MonoBehaviour {

	private Executor executor;
	private void Awake() {
		executor = FindObjectOfType<Executor>();
		Broker.Subscribe<SingleTapMessage>(OnSingleTapMessageReceived);
	}
	private void OnSingleTapMessageReceived(SingleTapMessage obj){
		// Consider Sending String
		if (obj.TappedObject.CompareTag("Valid")) {
			executor.Enqueue(new ValidAnswerCommand());
		}
		if (obj.TappedObject.CompareTag("Invalid")) {
			executor.Enqueue(new InvalidAnswerCommand());
		}
		if (obj.TappedObject.CompareTag("Exit")){
			executor.Enqueue(new ExitCommand());
		}
	}
	private bool ValidateTap(GameObject tappedObject){
		return tappedObject.CompareTag("Valid");
	}
}
