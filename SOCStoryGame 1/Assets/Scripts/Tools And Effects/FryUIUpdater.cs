using TMPro;
using UnityEngine;

public class FryUIUpdater : MonoBehaviour{
	private TextMeshProUGUI textDisplay;
	private int friesEaten;
	private void Awake(){
		textDisplay = GetComponent<TextMeshProUGUI>();
		Broker.Subscribe<ExecuteOnceMessage>(OnExecuteOnceMessageReceived);
	}
	
	private void OnDisable(){
		Broker.Unsubscribe<ExecuteOnceMessage>(OnExecuteOnceMessageReceived);
	}
	private void OnExecuteOnceMessageReceived(ExecuteOnceMessage obj){
		friesEaten++;
		textDisplay.text = $"{friesEaten}/10";
	}
	

}
