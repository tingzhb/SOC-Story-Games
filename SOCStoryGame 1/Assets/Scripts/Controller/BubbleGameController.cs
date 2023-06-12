using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BubbleGameController : MonoBehaviour {

	[SerializeField] private GameObject[] stars;
	[SerializeField] private float gameDuration;
	[SerializeField] private GameObject wellDone, UI;
	private int bubbleCount;
	private float currentGameDuration;

	private void Awake(){
		bubbleCount = 0;
		Broker.Subscribe<ExecuteOnceMessage>(OnExecuteOnceMessageReceived);
	}

	void OnDisable(){
		Broker.Unsubscribe<ExecuteOnceMessage>(OnExecuteOnceMessageReceived);
	}
	private void Update() {
		if (currentGameDuration > gameDuration && bubbleCount < 3) {
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		} else {
			currentGameDuration += Time.deltaTime;
		}
	}
	private void OnExecuteOnceMessageReceived(ExecuteOnceMessage obj) {
		stars[bubbleCount].GetComponent<AnimateOnce>().StartAnimation();
		bubbleCount++;
		SoundMessage soundMessage = new(){
			SoundType = 3
		};
		Broker.InvokeSubscribers(typeof(SoundMessage), soundMessage);
		
		if (bubbleCount == 3) {
			UI.SetActive(false);
			wellDone.SetActive(true);
		}
	}
}
