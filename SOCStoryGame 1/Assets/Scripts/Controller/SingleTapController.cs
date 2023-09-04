using System;
using UnityEngine;

public class SingleTapController : MonoBehaviour {

	private void Awake() {
		Broker.Subscribe<SingleTapMessage>(OnSingleTapMessageReceived);
	}

	void OnDisable(){
		Broker.Unsubscribe<SingleTapMessage>(OnSingleTapMessageReceived);
	}
	private void OnSingleTapMessageReceived(SingleTapMessage obj) {
		ValidateTap(obj.TappedObject);
	}
	private void ValidateTap(string tappedObject){
		
		Debug.Log(tappedObject);
		
		switch (tappedObject){
			case "Valid":
				SuccessMessage successMessage = new() {};
				Broker.InvokeSubscribers(typeof(SuccessMessage), successMessage);				
				break;
			case "Invalid":
				InvalidMessage invalidMessage = new(){ };
				Broker.InvokeSubscribers(typeof(InvalidMessage), invalidMessage);
				break;
			case "Failure":
				FailureMessage failureMessage = new(){ };
				Broker.InvokeSubscribers(typeof(FailureMessage), failureMessage);				
				break;
			case "Exit":
				ExitMessage exitMessage = new(){ };
				Broker.InvokeSubscribers(typeof(ExitMessage), exitMessage);
				break;
			case "Back":
				BackMessage backMessage = new();
				Broker.InvokeSubscribers(typeof(BackMessage), backMessage);				
				break;
			case "Sound":
				SoundMessage soundMessage = new(){
					SoundType = 98
				};
				Broker.InvokeSubscribers(typeof(SoundMessage), soundMessage);				
				break;
			case "Bubble":
				ExecuteOnceMessage executeOnceMessage = new();
				Broker.InvokeSubscribers(typeof(ExecuteOnceMessage), executeOnceMessage);				
				break;
			case "StickL":
				StickMessage stickLMessage = new(){
					IsLeft = true
				};
				Broker.InvokeSubscribers(typeof(StickMessage), stickLMessage);				
				break;
			case "StickR":
				StickMessage stickRMessage = new(){
					IsLeft = false
				};
				Broker.InvokeSubscribers(typeof(StickMessage), stickRMessage);							
				break;
		}
	}
}
