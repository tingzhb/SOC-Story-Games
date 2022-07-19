using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FryGameController : MonoBehaviour{
	private int friesEaten;
	private float timer;
	private Executor executor;
	[SerializeField] private GameObject fish;
	private void Awake(){
		executor = FindObjectOfType<Executor>();
		Broker.Subscribe<CorrectMessage>(OnCorrectMessageReceived);
	}

	private void Update(){
		timer += Time.deltaTime;
		if (timer >= 25){
			RestartGame();
		}
	}
	
	private void OnCorrectMessageReceived(CorrectMessage obj){
		EatFry();
	}
	private void EatFry(){
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
		executor.Enqueue(new ValidAnswerCommand());
	}

	private IEnumerator DelayEating(){
		yield return new WaitForSeconds(0.3f);
		fish.GetComponent<SpriteSwitcher>().enabled = true;
	}
	
	public void RestartGame(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	private void OnDestroy(){
		Broker.Unsubscribe<CorrectMessage>(OnCorrectMessageReceived);
	}
}
