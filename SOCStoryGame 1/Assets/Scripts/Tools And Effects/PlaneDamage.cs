using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlaneDamage : MonoBehaviour{
	[SerializeField] private Image image;

	private void Awake(){
		Broker.Subscribe<FailureMessage>(OnFailureMessageReceived);
	}	
	private void OnDisable(){ 
		Broker.Unsubscribe<FailureMessage>(OnFailureMessageReceived);
	}
	private void OnFailureMessageReceived(FailureMessage obj){
		image.color = Color.red;
		StartCoroutine(ResetImage());
	}

	private IEnumerator ResetImage(){
		yield return new WaitForSeconds(0.25f);
		image.color = Color.white;
	}


}
