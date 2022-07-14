using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EggGameController : MonoBehaviour{
	[SerializeField] private GameObject[] eggUI;
	private int savedEggs, brokenEggs;
	private Executor executor;
	private void Start(){
		executor = FindObjectOfType<Executor>();
		Broker.Subscribe<EggMessage>(OnEggMessageReceived);
	}
	private void OnEggMessageReceived(EggMessage obj){
		if (obj.Saved){
			SaveEgg();
		}
		if (!obj.Saved){
			BreakEgg();
		}
		CheckGameStatus();
	}
	private void BreakEgg(){
		brokenEggs++;
	}
	private void SaveEgg(){
		eggUI[savedEggs].GetComponent<AnimateOnce>().StartAnimation();
		savedEggs++;
	}
	private void CheckGameStatus(){
		if (savedEggs == 5){
			executor.Enqueue(new ValidAnswerCommand());
		}
		if (brokenEggs >= 33){
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}

	private void OnDestroy(){
		Broker.Unsubscribe<EggMessage>(OnEggMessageReceived);
	}
}
