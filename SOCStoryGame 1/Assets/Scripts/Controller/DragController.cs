using UnityEngine;

public class DragController : MonoBehaviour{
	private GameObject dragObject;
	private bool canDrag;
	private void Start(){
		Broker.Subscribe<DragMessage>(OnStartDragMessageReceived);
	}
	private void OnStartDragMessageReceived(DragMessage obj){
		Debug.Log("draggable");
		dragObject = obj.DragObject;
		canDrag = obj.Dragging;
	}

	private void Update(){
		if (dragObject is not null && canDrag){
			dragObject.transform.position = Input.mousePosition;
		}
	}
	private void OnDestroy(){
		Broker.Unsubscribe<DragMessage>(OnStartDragMessageReceived);
	}
}
