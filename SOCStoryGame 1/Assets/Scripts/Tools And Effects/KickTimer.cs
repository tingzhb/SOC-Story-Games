using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickTimer : MonoBehaviour{
	[SerializeField] private float startTime, endTime;
	private float timer, kicks;
	private Executor executor;
	private bool kicked;

	private void Awake() {
		executor = FindObjectOfType<Executor>();
	}

	private void Update(){
		timer += Time.deltaTime;
	}

	public void TryKick(){
		if (!kicked && timer >= startTime && timer <= endTime){
			kicked = true;
			kicks++;
			executor.Enqueue(new CorrectCommand());
		}
		else{
			kicked = true;
			executor.Enqueue(new FailureCommand());
		}
	}
}