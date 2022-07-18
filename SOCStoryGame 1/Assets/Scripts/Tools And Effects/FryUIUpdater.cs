using TMPro;
using UnityEngine;

public class FryUIUpdater : MonoBehaviour{
	private TextMeshProUGUI textDisplay;
	private int friesEaten;
	private void Start(){
		textDisplay = GetComponent<TextMeshProUGUI>();
		Broker.Subscribe<CorrectMessage>(OnCorrectMessageReceived);
	}
	private void OnCorrectMessageReceived(CorrectMessage obj){
		friesEaten++;
		textDisplay.text = $"{friesEaten}/10";
	}
	
	private void OnDestroy(){
		Broker.Unsubscribe<CorrectMessage>(OnCorrectMessageReceived);
	}
}
