using UnityEngine;

public class DropVerification : MonoBehaviour{
	[SerializeField] private GameObject[] droppedItems;

	private int totalDropped;
	private Executor executor;
	private void Awake(){
		executor = FindObjectOfType<Executor>();
		Broker.Subscribe<DragMessage>(OnDragMessageReceived);
	}

	public void OnDragMessageReceived(DragMessage obj){
		foreach (var item in droppedItems){
			if (GetComponent<BoxCollider2D>().bounds.Contains(item.transform.position)){
				totalDropped++;
			}
		}
		if (totalDropped == 6){
			executor.Enqueue(new ValidAnswerCommand());
		}
		Debug.Log(totalDropped); 
		executor.Enqueue(new FailureCommand());
		totalDropped = 0;
	}
}
