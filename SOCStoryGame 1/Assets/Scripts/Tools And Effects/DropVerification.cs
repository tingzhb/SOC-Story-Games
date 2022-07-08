using UnityEngine;

public class DropVerification : MonoBehaviour{
	[SerializeField] private string item;

	private int totalDropped;
	private Executor executor;
	private BoxCollider2D collider;
	private void Awake(){
		executor = FindObjectOfType<Executor>();
		Broker.Subscribe<DragMessage>(OnDragMessageReceived);
		collider = GetComponent<BoxCollider2D>();
	}

	private void OnDragMessageReceived(DragMessage obj){
		if (obj is not null && !obj.CanDrag){
			var dragObjTransform = obj.DragObject.transform;
			if (collider.bounds.Contains(dragObjTransform.position) && obj.ItemName == item){
				dragObjTransform.position = transform.position;
				obj.DragObject.GetComponent<OnTapHold>().Lock();
				totalDropped++;
			} else {
			}
			if (totalDropped == 6){ 
				executor.Enqueue(new ValidAnswerCommand()); 
			} 
			Debug.Log(totalDropped);
		} 
	}
}
