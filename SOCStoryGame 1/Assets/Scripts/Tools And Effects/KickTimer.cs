using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickTimer : MonoBehaviour{
	[SerializeField] private float startTime, endTime;
	private float timer;
	private bool kicked, canKick;

	private void Awake() {
		Broker.Subscribe<EggMessage>(OnEggMessageReceived);
	}

	void OnDisable(){
		Broker.Unsubscribe<EggMessage>(OnEggMessageReceived);
	}
	private void OnEggMessageReceived(EggMessage obj){
		if (obj.Saved){
			canKick = true;
		}
		else{
			canKick = false;
		}
	}

	private void Update(){
		timer += Time.deltaTime;
	}

	public void TryKick(){
		if (!kicked && canKick){
			kicked = true;
			ExecuteOnceMessage executeOnceMessage = new();
			Broker.InvokeSubscribers(typeof(ExecuteOnceMessage), executeOnceMessage);		}
		else {
			kicked = true;
			FailureMessage failureMessage = new(){ };
			Broker.InvokeSubscribers(typeof(FailureMessage), failureMessage);
		}
	}
}