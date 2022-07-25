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
		Broker.Subscribe<CorrectMessage>(OnCorrectMessageReceived);
	}
	private void OnCorrectMessageReceived(CorrectMessage obj){
		invalidCount = 0;
	}

	private void OnSuccessMessageReceived(SuccessMessage obj){
		invalidCount = 0;
		SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
	}
	
	private void OnFailureMessageReceived(FailureMessage obj){
		FailureFeedback();
	}
	
	private void OnExitMessageReceived(ExitMessage obj){
		invalidCount = 0;
		SoundMessage soundMessage = new(){
			SoundType = 99
		};
		Broker.InvokeSubscribers(typeof(SoundMessage), soundMessage);
		
		SceneManager.LoadSceneAsync("LevelSelect");
	}
	
	private void OnBackMessageReceived(BackMessage obj){
		SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex - 1);
	}

	private void OnInvalidMessageReceived(InvalidMessage obj){
		invalidCount++;
		if (invalidCount == 5){
			FailureFeedback();
			invalidCount = 0;
		}
	}

	private void FailureFeedback(){
		SoundMessage soundMessage = new(){
			SoundType = 1
		};
		Broker.InvokeSubscribers(typeof(SoundMessage), soundMessage);

		AssistanceMessage assistanceMessage = new();
		Broker.InvokeSubscribers(typeof(AssistanceMessage), assistanceMessage);
		StartCoroutine(WaitToPlayVO());
	}

	private IEnumerator WaitToPlayVO(){
		yield return new WaitForSeconds(1);
		SoundMessage soundMessage = new(){
			SoundType = 98
		};
		Broker.InvokeSubscribers(typeof(SoundMessage), soundMessage);
	}
}
