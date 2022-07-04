using UnityEngine;
using UnityEngine.SceneManagement;

public class ProgressController : MonoBehaviour{
	
	// SCENE MANAGER BREAKS WHEN MULTIPLE PLAYERS ARE LOADED. DISABLE PLAYER BEFORE TESTING
	private void Awake(){
		Broker.Subscribe<SuccessMessage>(OnSuccessMessageReceived);
		Broker.Subscribe<FailureMessage>(OnFailureMessageReceived);
		Broker.Subscribe<ExitMessage>(OnExitMessageReceived);
		Broker.Subscribe<BackMessage>(OnBackMessageReceived);
	}

	private void OnSuccessMessageReceived(SuccessMessage obj){
		Debug.Log("Success!");
		SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
	}
	
	private void OnFailureMessageReceived(FailureMessage obj){
		Debug.Log("Failure!");
	}
	
	private void OnExitMessageReceived(ExitMessage obj){
		Debug.Log("Quit Level");
	}
	
	private void OnBackMessageReceived(BackMessage obj){
		Debug.Log("Back");
		SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex - 1);
	}
}
