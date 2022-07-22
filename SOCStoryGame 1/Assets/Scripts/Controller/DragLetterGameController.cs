using System.Collections;
using UnityEngine;

public class DragLetterGameController : MonoBehaviour{

	[SerializeField] private int letterCount;
	[SerializeField] private GameObject wellDone;
	private int successCount;
	private Executor executor;

	private void Start(){
		executor = FindObjectOfType<Executor>();
		Broker.Subscribe<CorrectMessage>(OnCorrectMessageReceived);
	}
	private void OnCorrectMessageReceived(CorrectMessage obj){
		successCount++;
		if (successCount == letterCount){
			wellDone.SetActive(true);
			StartCoroutine(DelayEnd());
		}
	}
	
	private IEnumerator DelayEnd() {
		yield return new WaitForSeconds(1.5f);
		executor.Enqueue(new ValidAnswerCommand());
	}
	private void OnDestroy(){
		Broker.Unsubscribe<CorrectMessage>(OnCorrectMessageReceived);
	}
}
