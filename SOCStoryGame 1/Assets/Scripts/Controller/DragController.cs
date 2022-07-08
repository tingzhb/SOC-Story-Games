using System;
using System.Collections;
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
		canDrag = obj.CanDrag;
	}

	private void Update(){
		if (dragObject is not null && canDrag){
			dragObject.transform.position = Input.mousePosition;
		}
	}
}
