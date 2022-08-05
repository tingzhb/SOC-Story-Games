 using System;
 using UnityEngine;
 using UnityEngine.UI;

 public class DropImageShower : MonoBehaviour{
	 [SerializeField] private GameObject shape; 
	 private Image imageResponse;
	 private BoxCollider2D boxCollider;
	 private bool success;
	 private void Awake(){
		 boxCollider = GetComponent<BoxCollider2D>();
		 imageResponse = GetComponent<Image>();
		 Broker.Subscribe<DragMessage>(OnDragMessageReceived);

	 }
	 private void OnDragMessageReceived(DragMessage obj){
		 if (!success && boxCollider.bounds.Contains(shape.transform.position)) {
			 success = true;
			 shape.SetActive(false);
			 imageResponse.color = Color.white;
			 ReportSuccess();
		 }
	 }
	 private void ReportSuccess(){
		 Debug.Log("Success");
		 CorrectMessage correctMessage = new();
		 Broker.InvokeSubscribers(typeof(CorrectMessage), correctMessage);
	 }
	 private void OnDestroy(){
		 Broker.Unsubscribe<DragMessage>(OnDragMessageReceived);
	 }
 }