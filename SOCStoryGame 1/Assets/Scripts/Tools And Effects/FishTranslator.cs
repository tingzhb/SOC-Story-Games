using UnityEngine;

public class FishTranslator : MonoBehaviour{
	private float timer;
	[SerializeField] private Transform stopPoint, endPoint;
	[SerializeField] private float movementSpeed;
	[SerializeField] private GameObject fish;


	private void Awake(){
		Broker.Subscribe<SingleTapMessage>(OnSingleTapMessageReceived);
	}
	private void Update(){
		timer += Time.deltaTime;
		if (fish.transform.position.x < stopPoint.transform.position.x && timer < 5){
			fish.transform.Translate(Vector3.right * (movementSpeed * Time.deltaTime));
		}
		if (fish.transform.position.x < endPoint.position.x && timer > 4){
			fish.transform.Translate(Vector3.right * (movementSpeed * Time.deltaTime));
		}
		if (timer > 5){
			Destroy(gameObject);
		}
	}
	
	private void OnSingleTapMessageReceived(SingleTapMessage obj){
		if (obj.TappedObject == "Bubble"){
			Destroy(gameObject);
		}
	}
	private void OnDestroy(){
		Broker.Unsubscribe<SingleTapMessage>(OnSingleTapMessageReceived);
	}
}
