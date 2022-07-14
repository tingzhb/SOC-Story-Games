using UnityEngine;

public class DragController : MonoBehaviour{
	private GameObject dragObject;
	[SerializeField] private bool lockY;
	private void Start(){
		Broker.Subscribe<DragMessage>(OnStartDragMessageReceived);
	}
	private void OnStartDragMessageReceived(DragMessage obj){
		Debug.Log("draggable");
		dragObject = obj.DragObject;
	}

	private void Update(){
		if (dragObject is not null){
			if (lockY){
				var position = dragObject.transform.position;
				position = new Vector3(Input.mousePosition.x, position.y, position.z);
				dragObject.transform.position = position;
			} else {
				dragObject.transform.position = Input.mousePosition;
			}
		}
	}
	private void OnDestroy(){
		Broker.Unsubscribe<DragMessage>(OnStartDragMessageReceived);
	}
}
