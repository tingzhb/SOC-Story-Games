using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticNext : MonoBehaviour{
	[SerializeField] private float delay;
	private float timer;
	
	private void Update(){
		timer += Time.deltaTime;
		if (timer >= delay){
			SuccessMessage successMessage = new() {};
			Broker.InvokeSubscribers(typeof(SuccessMessage), successMessage);
			timer = 0;
		}
	}
}
