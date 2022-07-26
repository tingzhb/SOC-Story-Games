using UnityEngine;
using UnityEngine.EventSystems;

public class OnTapHold : MonoBehaviour, IPointerDownHandler, IPointerUpHandler{
	[SerializeField] private bool playSound;
	public void OnPointerDown(PointerEventData eventData){
		StartDrag();
	}
	private void StartDrag(){
		DragMessage dragMessage = new(){DragObject = gameObject};
		Broker.InvokeSubscribers(typeof(DragMessage), dragMessage);
		
	}

	public void OnPointerUp(PointerEventData eventData){
		StopDrag();
	}
	
	private void StopDrag(){
		DragMessage dragMessage = new(){ };
		Broker.InvokeSubscribers(typeof(DragMessage), dragMessage);
		if (playSound){
			SoundMessage soundMessage = new(){SoundType = 5};
			Broker.InvokeSubscribers(typeof(SoundMessage), soundMessage);
		}
	}
}