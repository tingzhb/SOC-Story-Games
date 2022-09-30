using UnityEngine;

public class DolphinKiller : MonoBehaviour {
	private void Awake(){
		Broker.Subscribe<CorrectMessage>(OnCorrectMessageReceived);
		Broker.Subscribe<FailureMessage>(OnFailureMessageReceived);
	}
	private void OnDisable(){
		Broker.Unsubscribe<CorrectMessage>(OnCorrectMessageReceived);
		Broker.Unsubscribe<FailureMessage>(OnFailureMessageReceived);
	}
	private void OnFailureMessageReceived(FailureMessage obj){
		Destroy(gameObject);
	}
	private void OnCorrectMessageReceived(CorrectMessage obj){
		Destroy(gameObject);
	}
}
