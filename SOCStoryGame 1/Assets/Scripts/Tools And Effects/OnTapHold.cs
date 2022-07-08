using UnityEngine;
using UnityEngine.EventSystems;

public class OnTapHold : MonoBehaviour, IPointerDownHandler, IPointerUpHandler{
	public void OnPointerDown(PointerEventData eventData){
		DragMessage dragMessage = new(){
			CanDrag = true,
			DragObject = gameObject
		};
		Broker.InvokeSubscribers(typeof(DragMessage), dragMessage);
	}
	public void OnPointerUp(PointerEventData eventData){
		DragMessage dragMessage = new(){
			CanDrag = false
		};
		Broker.InvokeSubscribers(typeof(DragMessage), dragMessage);	}
}