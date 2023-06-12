using System;
using System.Collections;
using UnityEngine;

public class DragLetterGameController : MonoBehaviour{

	[SerializeField] private int letterCount;
	[SerializeField] private GameObject wellDone;
	private int successCount;

	private void Awake(){
		Broker.Subscribe<ExecuteOnceMessage>(OnExecuteOnceMessageReceived);
	}
	void OnDisable(){
		Broker.Unsubscribe<ExecuteOnceMessage>(OnExecuteOnceMessageReceived);
	}
	private void OnExecuteOnceMessageReceived(ExecuteOnceMessage obj){
		successCount++;
		if (successCount == letterCount){
			wellDone.SetActive(true);
			StartCoroutine(DelayEnd());
		}
	}
	
	private IEnumerator DelayEnd() {
		yield return new WaitForSeconds(1.5f);
		SuccessMessage successMessage = new() {};
		Broker.InvokeSubscribers(typeof(SuccessMessage), successMessage);	
	}
}
