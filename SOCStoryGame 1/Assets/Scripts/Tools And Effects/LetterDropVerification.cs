 using System;
 using UnityEngine;

 public class LetterDropVerification : MonoBehaviour{
	 [SerializeField] private Transform[] letters;
	 private BoxCollider2D boxCollider;
	 private bool success;
	 private void Awake() {
		 boxCollider = GetComponent<BoxCollider2D>();
	 }

	 private void Start(){
		 Broker.Subscribe<DragMessage>(OnDragMessageReceived);
	 }
	 private void OnDragMessageReceived(DragMessage obj){
		 if (!success && (boxCollider.bounds.Contains(letters[0].position) || boxCollider.bounds.Contains(letters[1].position))) {
			 success = true;
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
