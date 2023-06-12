using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlightGameController : MonoBehaviour{
	[SerializeField] private int steps;
	[SerializeField] private GameObject wellDone;
	private int failureCount;

	private void Awake(){
		Broker.Subscribe<ExecuteOnceMessage>(OnExecuteOnceMessageReceived);
		Broker.Subscribe<FailureMessage>(OnFailureMessageReceived);
	}

	void OnDisable(){
		Time.timeScale = 1;
		Broker.Unsubscribe<ExecuteOnceMessage>(OnExecuteOnceMessageReceived);
		Broker.Unsubscribe<FailureMessage>(OnFailureMessageReceived);
	}
	private void OnFailureMessageReceived(FailureMessage obj){
		failureCount++;
		if (failureCount == 5){
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}
	private void OnExecuteOnceMessageReceived(ExecuteOnceMessage obj){
		steps--;
		if (steps == 0) {
			wellDone.SetActive(true);
			StartCoroutine(DelayEnd());
		}
	}
	
	private IEnumerator DelayEnd() {
		yield return new WaitForSeconds(1.5f);
		SuccessMessage successMessage = new() {};
		Broker.InvokeSubscribers(typeof(SuccessMessage), successMessage);	}
}