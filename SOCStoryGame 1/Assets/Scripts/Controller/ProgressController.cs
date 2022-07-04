using UnityEngine;

public class ProgressController : MonoBehaviour {
	private void Awake(){
		Broker.Subscribe<SuccessMessage>(OnSuccessMessageReceived);
		Broker.Subscribe<FailureMessage>(OnFailureMessageReceived);
	}

	private void OnSuccessMessageReceived(SuccessMessage obj){
		Debug.Log("Success!");
	}
	
	private void OnFailureMessageReceived(FailureMessage obj){
		Debug.Log("Failure!");
	}
}
