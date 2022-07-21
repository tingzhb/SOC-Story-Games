using UnityEngine;

public class DragController : MonoBehaviour{
	private GameObject dragObject;
	[SerializeField] private bool lockY, lockX;
	private void Awake(){
		Broker.Subscribe<DragMessage>(OnStartDragMessageReceived);
	}
	private void OnStartDragMessageReceived(DragMessage obj){
		dragObject = obj.DragObject;
	}

	private void Update(){
		if (dragObject is not null){
			if (lockX){
				var position = dragObject.transform.position;
				position = new Vector3(position.x, Input.mousePosition.y, position.z);
				dragObject.transform.position = position;
			} else if (lockY){
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
