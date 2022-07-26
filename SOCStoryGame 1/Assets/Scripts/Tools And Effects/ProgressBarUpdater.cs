using UnityEngine;

public class ProgressBarUpdater : MonoBehaviour{
	[SerializeField] private Transform[] steps;
	[SerializeField] private GameObject icon;
	private int stepCount;
	private void Start(){
		Broker.Subscribe<CorrectMessage>(OnCorrectMessageReceived);
	}
	private void OnCorrectMessageReceived(CorrectMessage obj){
		if (stepCount < steps.Length){
			icon.transform.position = steps[stepCount].position;
		}
		stepCount++;
	}

	private void OnDestroy(){
		Broker.Unsubscribe<CorrectMessage>(OnCorrectMessageReceived);
	}
}
