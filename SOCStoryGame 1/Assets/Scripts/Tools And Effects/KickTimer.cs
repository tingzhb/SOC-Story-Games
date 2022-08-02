using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickTimer : MonoBehaviour{
	[SerializeField] private float startTime, endTime;
	private float timer;
	private Executor executor;
	private bool kicked, canKick;

	private void Awake() {
		Broker.Subscribe<EggMessage>(OnEggMessageReceived);
		executor = FindObjectOfType<Executor>();
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
			executor.Enqueue(new CorrectCommand());
		}
		else {
			kicked = true;
			executor.Enqueue(new FailureCommand());
		}
	}
}