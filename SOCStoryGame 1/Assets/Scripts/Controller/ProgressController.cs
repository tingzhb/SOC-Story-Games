using UnityEngine;
using UnityEngine.SceneManagement;

public class ProgressController : MonoBehaviour{
	private int sceneNumber = 1;
	private void Awake(){
		Broker.Subscribe<SuccessMessage>(OnSuccessMessageReceived);
		Broker.Subscribe<FailureMessage>(OnFailureMessageReceived);
		Broker.Subscribe<ExitMessage>(OnExitMessageReceived);
	}

	private void OnSuccessMessageReceived(SuccessMessage obj){
		Debug.Log("Success!");
		SceneManager.LoadScene(sceneNumber++);
	}
	
	private void OnFailureMessageReceived(FailureMessage obj){
		Debug.Log("Failure!");
	}
	
	private void OnExitMessageReceived(ExitMessage obj){
		Debug.Log("Quit Level");
	}

}
