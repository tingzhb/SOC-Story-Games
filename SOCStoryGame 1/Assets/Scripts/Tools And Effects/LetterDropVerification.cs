 using System;
 using UnityEngine;

 public class LetterDropVerification : MonoBehaviour{
	 [SerializeField] private Transform[] letters;
	 private BoxCollider2D boxCollider;
	 private bool success;
	 private void Awake() {
		 boxCollider = GetComponent<BoxCollider2D>();
		 Broker.Subscribe<DragMessage>(OnDragMessageReceived);
	 }

	 void OnDisable(){
		 Broker.Unsubscribe<DragMessage>(OnDragMessageReceived);
	 }

	 private void OnDragMessageReceived(DragMessage obj){
		 if (!success && (boxCollider.bounds.Contains(letters[0].position) || boxCollider.bounds.Contains(letters[1].position))) {
			 success = true;
			 ReportSuccess();
		 }
	 }
	 private void ReportSuccess(){
		 ExecuteOnceMessage executeOnceMessage = new();
		 Broker.InvokeSubscribers(typeof(ExecuteOnceMessage), executeOnceMessage);
	 }
 }
