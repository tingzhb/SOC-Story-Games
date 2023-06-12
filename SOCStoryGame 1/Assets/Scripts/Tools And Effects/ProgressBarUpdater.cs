using UnityEngine;

public class ProgressBarUpdater : MonoBehaviour{
	[SerializeField] private Transform[] steps;
	[SerializeField] private GameObject icon;
	private int stepCount;
	private void Awake(){
		Broker.Subscribe<ExecuteOnceMessage>(OnExecuteOnceMessageReceived);
	}
	private void OnExecuteOnceMessageReceived(ExecuteOnceMessage obj){
		if (stepCount < steps.Length){
			icon.transform.position = steps[stepCount].position;
		}
		stepCount++;
	}

	private void OnDestroy(){
		Broker.Unsubscribe<ExecuteOnceMessage>(OnExecuteOnceMessageReceived);
	}
}
