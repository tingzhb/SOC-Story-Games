 using UnityEngine;

 public class DropVerification : MonoBehaviour{
	 [SerializeField] private GameObject[] candles;
	 private int totalDropped;
	 private BoxCollider2D boxCollider;
	 private void Awake() {
		 boxCollider = GetComponent<BoxCollider2D>();
	 }

	 public void Verify(){
		 foreach (var candle in candles){
			 if (boxCollider.bounds.Contains(candle.transform.position)){
				 totalDropped++;
			 }
		 }
		 if (totalDropped == 6){
			 SuccessMessage successMessage = new() {};
			 Broker.InvokeSubscribers(typeof(SuccessMessage), successMessage);
			 
		 } else {
			 FailureMessage failureMessage = new(){ };
			 Broker.InvokeSubscribers(typeof(FailureMessage), failureMessage);	
			 totalDropped = 0;
		 }
	 }
 }
