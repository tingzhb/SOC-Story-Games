using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FryGameController : MonoBehaviour{
	private int friesEaten;
	private float timer;
	[SerializeField] private GameObject fish;
	private void Awake(){
		Broker.Subscribe<ExecuteOnceMessage>(OnExecuteOnceMessageReceived);
	}
	
	private void OnDisable(){
		Broker.Unsubscribe<ExecuteOnceMessage>(OnExecuteOnceMessageReceived);
	}

	private void Update(){
		timer += Time.deltaTime;
		if (timer >= 25){
			RestartGame();
		}
	}
	
	private void OnExecuteOnceMessageReceived(ExecuteOnceMessage obj){
		EatFry();
	}
	private void EatFry(){
		SoundMessage soundMessage = new(){
			SoundType = 8
		};
		Broker.InvokeSubscribers(typeof(SoundMessage), soundMessage);
		fish.GetComponent<SpriteSwitcher>().enabled = false;
		fish.GetComponent<AnimateOnce>().StartAnimation();
		StartCoroutine(DelayEating());
		
		friesEaten++;
		if (friesEaten >= 10){
			StartCoroutine(DelayEnd());
		}
	}
	
	private IEnumerator DelayEnd(){
		yield return new WaitForSeconds(0.5f);
		SuccessMessage successMessage = new() {};
		Broker.InvokeSubscribers(typeof(SuccessMessage), successMessage);
		
	}

	private IEnumerator DelayEating(){
		yield return new WaitForSeconds(0.3f);
		fish.GetComponent<SpriteSwitcher>().enabled = true;
	}
	
	public void RestartGame(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
