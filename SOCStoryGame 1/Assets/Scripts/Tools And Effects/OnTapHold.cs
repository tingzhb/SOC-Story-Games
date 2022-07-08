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

	public void OnPointerDown(PointerEventData eventData){
		transform.localScale = Vector3.one;
		if (!locked){
			DragMessage dragMessage = new(){
				CanDrag = true,
				DragObject = gameObject
			};
			Broker.InvokeSubscribers(typeof(DragMessage), dragMessage);
		}
	}
	public void OnPointerUp(PointerEventData eventData){
		if (!locked){
			transform.localScale = Vector3.one * 0.5f;
			DragMessage dragMessage = new(){
				CanDrag = false,
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