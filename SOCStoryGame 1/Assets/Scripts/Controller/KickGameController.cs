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

	private void Awake(){
		ballScore = 0;
		Broker.Subscribe<ExecuteOnceMessage>(OnExecuteOnceMessageReceived);
		CreateBar();
	}

	void OnDisable(){
		Broker.Unsubscribe<ExecuteOnceMessage>(OnExecuteOnceMessageReceived);
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
	private void OnExecuteOnceMessageReceived(ExecuteOnceMessage obj){
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
		SuccessMessage successMessage = new() {};
		Broker.InvokeSubscribers(typeof(SuccessMessage), successMessage);
	}
	
}
