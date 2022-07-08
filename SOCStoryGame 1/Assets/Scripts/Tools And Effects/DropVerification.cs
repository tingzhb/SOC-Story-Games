using UnityEngine;

public class DropVerification : MonoBehaviour{
	[SerializeField] private string item;

	private int totalDropped;
	private BoxCollider2D boxCollider;
	private void Awake(){
		Broker.Subscribe<DragMessage>(OnDragMessageReceived);
		boxCollider = GetComponent<BoxCollider2D>();
	}

	private void OnDragMessageReceived(DragMessage obj){
		if (obj is not null && !obj.Dragging){
			var dragObjTransform = obj.DragObject.transform;
			if (boxCollider.bounds.Contains(dragObjTransform.position) && obj.ItemName == item){
				dragObjTransform.position = transform.position;
				obj.DragObject.GetComponent<OnTapHold>().Lock();
				obj.DragObject.transform.localScale = Vector3.one * 0.5f;
				Broker.Unsubscribe<DragMessage>(OnDragMessageReceived);
			}
		} 
	}
}
