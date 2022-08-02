using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class KickGameController : MonoBehaviour {

	[SerializeField] private Image[] ballUI;
	[SerializeField] private Sprite ballDone;
	[SerializeField] private float barTime;
	[SerializeField] private GameObject bar, wellDone;
	[SerializeField] private AnimateOnce animateOnce;
	private GameObject barInstance;
	private int ballScore;
	private float timePassed;
	private Executor executor;

	private void Awake(){
		executor = FindObjectOfType<Executor>();
		ballScore = 0;
		Broker.Subscribe<CorrectMessage>(OnCorrectMessageReceived);
		CreateBar();
	}

	private void Update(){
		timePassed += Time.deltaTime;
		if (ballScore < 5 && timePassed >= barTime){
			timePassed = 0;
			Destroy(barInstance);
			CreateBar();
		}
	}

	private void CreateBar(){
		var spawnPoint = transform;
		barInstance = Instantiate(bar, spawnPoint.position, Quaternion.identity, spawnPoint);
	}
	private void OnCorrectMessageReceived(CorrectMessage obj){
		ballUI[ballScore].sprite = ballDone;
		ballScore++;
		SoundMessage soundMessage = new(){
			SoundType = 6
		};
		Broker.InvokeSubscribers(typeof(SoundMessage), soundMessage);
		
		animateOnce.StartAnimation();
		if (ballScore == 5){
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
