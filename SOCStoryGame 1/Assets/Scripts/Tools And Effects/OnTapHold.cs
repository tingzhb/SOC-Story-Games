using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnTapHold : MonoBehaviour, IPointerDownHandler, IPointerUpHandler{
	private bool locked;
	[SerializeField] private string item;
	[SerializeField] private GameObject origin;
	private void FixedUpdate(){
		if (!locked){
			transform.position = origin.transform.position;
		}
	}

	private void Update(){
		if (Input.touchCount < 1 && !Input.GetMouseButton(0)){
			StopDrag();
		}
	}

	public void OnPointerDown(PointerEventData eventData){
		StartDrag();
	}
	private void StartDrag(){
		transform.localScale = Vector3.one;
		if (!locked){
			DragMessage dragMessage = new(){
				Dragging = true,
				DragObject = gameObject
			};
			Broker.InvokeSubscribers(typeof(DragMessage), dragMessage);
		}
	}

	public void OnPointerUp(PointerEventData eventData){
		StopDrag();
	}
	private void StopDrag(){
		if (!locked){
			DragMessage dragMessage = new(){
				Dragging = false,
				DragObject = gameObject,
				ItemName = item
			};
			Broker.InvokeSubscribers(typeof(DragMessage), dragMessage);
		}
	}
	public void Lock(){
		transform.localScale = Vector3.one;
		locked = true;
		gameObject.tag = "Untagged";
		InPlaceMessage inPlaceMessage = new();
		Broker.InvokeSubscribers(typeof(InPlaceMessage), inPlaceMessage);
	}
}