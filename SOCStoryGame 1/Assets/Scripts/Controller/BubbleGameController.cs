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

	private void Start(){
		bubbleCount = 0;
		Broker.Subscribe<CorrectMessage>(OnCorrectMessageReceived);
	}

	private void Update() {
		if (currentGameDuration > gameDuration && bubbleCount < 3) {
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		} else {
			currentGameDuration += Time.deltaTime;
		}
	}
	private void OnCorrectMessageReceived(CorrectMessage obj) {
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

	private void OnDestroy(){
		Broker.Unsubscribe<CorrectMessage>(OnCorrectMessageReceived);
	}
}
