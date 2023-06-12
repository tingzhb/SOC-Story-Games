using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EggGameController : MonoBehaviour{
	[SerializeField] private GameObject[] eggUI;
	[SerializeField] private GameObject failureUI;
	private int savedEggs, brokenEggs;
	private void Awake(){
		Broker.Subscribe<EggMessage>(OnEggMessageReceived);
	}

	void OnDisable(){
		Broker.Unsubscribe<EggMessage>(OnEggMessageReceived);
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
		SoundMessage soundMessage = new(){SoundType = 6};
		Broker.InvokeSubscribers(typeof(SoundMessage), soundMessage);
		savedEggs++;
	}
	private void CheckGameStatus(){
		if (savedEggs == 5){
			SuccessMessage successMessage = new() {};
			Broker.InvokeSubscribers(typeof(SuccessMessage), successMessage);
			
		}
		if (brokenEggs >= 4){
			failureUI.SetActive(true);
			FailureMessage failureMessage = new(){ };
			Broker.InvokeSubscribers(typeof(FailureMessage), failureMessage);				
			brokenEggs = -2;
		}
	}
	public void RestartGame(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
