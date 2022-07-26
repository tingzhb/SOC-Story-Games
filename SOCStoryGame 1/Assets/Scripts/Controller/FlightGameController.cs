using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlightGameController : MonoBehaviour {

	[SerializeField] private Transform[] steps;
	[SerializeField] private GameObject planeIcon;
	private int stepCount, failureCount;
	private Executor executor;
	
	private void Start(){
		Broker.Subscribe<CorrectMessage>(OnCorrectMessageReceived);
		Broker.Subscribe<FailureMessage>(OnFailureMessageReceived);
	}
	private void OnFailureMessageReceived(FailureMessage obj){
		failureCount++;
		if (failureCount == 5){
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}
	private void OnCorrectMessageReceived(CorrectMessage obj){
		if (stepCount < steps.Length){
			planeIcon.transform.position = steps[stepCount].transform.position;
		}
		stepCount++;
		
		if (stepCount == steps.Length - 1) {
			StartCoroutine(DelayEnd());
		}
	}
	
	private IEnumerator DelayEnd() {
		yield return new WaitForSeconds(1.5f);
		executor.Enqueue(new ValidAnswerCommand());
	}
	private void OnDestroy(){
		Time.timeScale = 1;
		Broker.Unsubscribe<CorrectMessage>(OnCorrectMessageReceived);
	}
}
