using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlightGameController : MonoBehaviour{
	[SerializeField] private int steps;
	[SerializeField] private GameObject wellDone;
	private int failureCount;
	private Executor executor;

	private void Start(){
		Broker.Subscribe<CorrectMessage>(OnCorrectMessageReceived);
		Broker.Subscribe<FailureMessage>(OnFailureMessageReceived);
		executor = FindObjectOfType<Executor>();
	}
	private void OnFailureMessageReceived(FailureMessage obj){
		failureCount++;
		if (failureCount == 5){
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}
	private void OnCorrectMessageReceived(CorrectMessage obj){
		steps--;
		if (steps == 0) {
			wellDone.SetActive(true);
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