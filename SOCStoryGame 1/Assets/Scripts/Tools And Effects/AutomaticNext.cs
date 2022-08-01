using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticNext : MonoBehaviour{
	[SerializeField] private float delay;
	private float timer;
	private Executor executor;

	private void Awake(){
		executor = FindObjectOfType<Executor>();
	}

	private void Update(){
		timer += Time.deltaTime;
		if (timer >= delay){
			executor.Enqueue(new ValidAnswerCommand());
			timer = 0;
		}
	}
}
