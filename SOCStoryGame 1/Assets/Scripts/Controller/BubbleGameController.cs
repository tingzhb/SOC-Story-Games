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
		Broker.Subscribe<BubbleMessage>(OnBubbleMessageReceived);
	}

	private void Update() {
		if (currentGameDuration > gameDuration && bubbleCount < 3) {
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		} else {
			currentGameDuration += Time.deltaTime;
		}
	}
	private void OnBubbleMessageReceived(BubbleMessage obj) {
		stars[bubbleCount].GetComponent<AnimateOnce>().StartAnimation();
		bubbleCount++;
		if (bubbleCount == 3) {
			UI.SetActive(false);
			wellDone.SetActive(true);
			Broker.Unsubscribe<BubbleMessage>(OnBubbleMessageReceived);
			StartCoroutine(DelayEnd());
		}
	}
	private IEnumerator DelayEnd() {
		yield return new WaitForSeconds(2f);
		Time.timeScale = 0;
	}
}
