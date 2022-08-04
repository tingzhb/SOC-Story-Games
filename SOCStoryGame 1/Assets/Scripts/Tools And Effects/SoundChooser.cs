using System;
using System.Collections;
using UnityEngine;

public class SoundChooser : MonoBehaviour{
	private bool canClick = true;

	private void Awake(){
		Broker.Subscribe<FailureMessage>(OnFailureMessageReceived);
	}
	private void OnFailureMessageReceived(FailureMessage obj){
		canClick = false;
		StartCoroutine(HoldClick());
	}

	private IEnumerator HoldClick(){
		yield return new WaitForSeconds(1);
		canClick = true;
	}

	public void ChooseCow(){
		if (canClick){
			EggMessage eggMessage = new(){
				Saved = false
			};
			Broker.InvokeSubscribers(typeof(EggMessage), eggMessage);
		
			SoundMessage soundMessage = new(){
				SoundType = 10
			};
			Broker.InvokeSubscribers(typeof(SoundMessage), soundMessage);
		}
	}
	
	public void ChooseCat(){
		if (canClick){
			EggMessage eggMessage = new(){
				Saved = true
			};
			Broker.InvokeSubscribers(typeof(EggMessage), eggMessage);
		
			SoundMessage soundMessage = new(){
				SoundType = 9
			};
			Broker.InvokeSubscribers(typeof(SoundMessage), soundMessage);	
		}
	}
}
