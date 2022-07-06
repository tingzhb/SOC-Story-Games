using System.Collections;
using UnityEngine;

public class BubbleGameController : MonoBehaviour{

	[SerializeField] private GameObject[] stars;
	private int bubbleCount;
	private Executor executor;

	private void Awake(){
		executor = FindObjectOfType<Executor>();
		Broker.Subscribe<BubbleMessage>(OnBubbleMessageReceived);
	}
	private void OnBubbleMessageReceived(BubbleMessage obj){
		stars[bubbleCount].SetActive(true);
		bubbleCount++;
		if (bubbleCount == 3){
			StartCoroutine(DelayEnd());
		}
	}
	private IEnumerator DelayEnd(){
		yield return new WaitForSeconds(1f);
		executor.Enqueue(new ValidAnswerCommand());
	}
}
