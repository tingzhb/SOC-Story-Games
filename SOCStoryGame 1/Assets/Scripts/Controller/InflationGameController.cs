using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InflationGameController : MonoBehaviour {

	[SerializeField] private Image[] bubbleUI;
	[SerializeField] private Sprite bubbleDone;
	[SerializeField] private GameObject chloe, wellDone;
	private GameObject chloeInstance;
	private int bubbleScore;
	private Executor executor;

	private void Awake(){
		executor = FindObjectOfType<Executor>();
		bubbleScore = 0;
		Broker.Subscribe<CorrectMessage>(OnCorrectMessageReceived);
		CreateChloe();
	}
	
	private void CreateChloe(){
		var spawnPoint = transform;
		chloeInstance = Instantiate(chloe, spawnPoint.position, Quaternion.identity, spawnPoint);
	}
	private void OnCorrectMessageReceived(CorrectMessage obj){
		Destroy(chloeInstance);
		CreateChloe();
		bubbleUI[bubbleScore].sprite = bubbleDone;
		bubbleScore++;

		if (bubbleScore == 5){
			wellDone.SetActive(true);
			StartCoroutine(DelayEnd());
		}
	}
	private IEnumerator DelayEnd(){
		yield return new WaitForSeconds(2.5f);
		executor.Enqueue(new ValidAnswerCommand());
	}

	private void OnDestroy(){
		Broker.Unsubscribe<CorrectMessage>(OnCorrectMessageReceived);
	}
}
