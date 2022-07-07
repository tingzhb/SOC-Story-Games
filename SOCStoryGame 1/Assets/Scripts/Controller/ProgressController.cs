using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProgressController : MonoBehaviour{

	private int invalidCount;

	// SCENE MANAGER BREAKS WHEN MULTIPLE PLAYERS ARE LOADED. DISABLE PLAYER BEFORE TESTING
	private void Awake(){
		Broker.Subscribe<SuccessMessage>(OnSuccessMessageReceived);
		Broker.Subscribe<FailureMessage>(OnFailureMessageReceived);
		Broker.Subscribe<ExitMessage>(OnExitMessageReceived);
		Broker.Subscribe<BackMessage>(OnBackMessageReceived);
		Broker.Subscribe<InvalidMessage>(OnInvalidMessageReceived);
	}

	private void OnSuccessMessageReceived(SuccessMessage obj){
		Debug.Log("Success!");
		SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
	}
	
	private void OnFailureMessageReceived(FailureMessage obj){
		Debug.Log("Failure!");
		FailureFeedback();
	}
	
	private void OnExitMessageReceived(ExitMessage obj){
		Debug.Log("Quit Level");
		SoundMessage soundMessage = new(){
			SoundType = 99
		};
		Broker.InvokeSubscribers(typeof(SoundMessage), soundMessage);
		
		SceneManager.LoadSceneAsync("LevelSelect");
		// Application.Quit();
	}
	
	private void OnBackMessageReceived(BackMessage obj){
		Debug.Log("Back");
		SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex - 1);
	}

	private void OnInvalidMessageReceived(InvalidMessage obj){
		Debug.Log("Invalid!");
		invalidCount++;
		if (invalidCount == 3){
			FailureFeedback();
			invalidCount = 0;
		}
	}

	private void FailureFeedback(){
		SoundMessage soundMessage = new(){
			SoundType = 1
		};
		Broker.InvokeSubscribers(typeof(SoundMessage), soundMessage);
		StartCoroutine(WaitToPlayVO());
	}

	private IEnumerator WaitToPlayVO(){
		yield return new WaitForSeconds(1);
		SoundMessage soundMessage = new(){
			SoundType = 2
		};
		Broker.InvokeSubscribers(typeof(SoundMessage), soundMessage);
	}
}
