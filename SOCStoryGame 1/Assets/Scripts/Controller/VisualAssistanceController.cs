using System;
using System.Collections;
using UnityEngine;

public class VisualAssistanceController : MonoBehaviour{
	[SerializeField] private GameObject assistance;
	[SerializeField] private bool canAssist = true;

	private void Awake(){
		Broker.Subscribe<AssistanceMessage>(OnAssistanceMessageReceived);
	}
	private void OnAssistanceMessageReceived(AssistanceMessage obj){
		if (canAssist){
			assistance.SetActive(true);
			StartCoroutine(DelayDeactivate());
		}
	}
	private IEnumerator DelayDeactivate(){
		yield return new WaitForSeconds(0.5f);
		assistance.SetActive(false);
	}
	private void OnDestroy(){
		Broker.Unsubscribe<AssistanceMessage>(OnAssistanceMessageReceived);
	}
}
