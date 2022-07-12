 using UnityEngine;

 public class DropVerification : MonoBehaviour{
	 [SerializeField] private GameObject[] candles;
	 private Executor executor;
	 private int totalDropped;
	 private BoxCollider2D boxCollider;
	 private void Awake() {
		 boxCollider = GetComponent<BoxCollider2D>();
		 executor = FindObjectOfType<Executor>();
	 }

	 public void Verify(){
		 foreach (var candle in candles){
			 if (boxCollider.bounds.Contains(candle.transform.position)){
				 totalDropped++;
			 }
		 }
		 if (totalDropped == 6){
			 executor.Enqueue(new ValidAnswerCommand());

		 } else {
			 executor.Enqueue(new FailureCommand());
			 totalDropped = 0;
		 }
	 }
 }
