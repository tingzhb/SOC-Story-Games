using UnityEngine;

public class SingleTapController : MonoBehaviour {

	private Executor executor;
	private void Awake() {
		executor = FindObjectOfType<Executor>();
		Broker.Subscribe<SingleTapMessage>(OnSingleTapMessageReceived);
	}
	private void OnSingleTapMessageReceived(SingleTapMessage obj){
		if (ValidateTap(obj.TappedObject)){
			executor.Enqueue(new ValidAnswerCommand());
		}
		else {
			executor.Enqueue(new InvalidAnswerCommand());
		}
	}
	private bool ValidateTap(GameObject tappedObject){
		return tappedObject.CompareTag("Valid");
	}
}
