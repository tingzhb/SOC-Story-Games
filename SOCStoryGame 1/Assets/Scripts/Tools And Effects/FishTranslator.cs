using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FishTranslator : MonoBehaviour{
	private float timer, sWidth;
	[SerializeField] private Transform stopPoint, endPoint;
	[SerializeField] private float movementSpeed;
	[SerializeField] private GameObject fish, fishReward;
	private Image img;


	private void Awake(){
		sWidth = Screen.width;
		Broker.Subscribe<SingleTapMessage>(OnSingleTapMessageReceived);

	}
	private void Update(){
		timer += Time.deltaTime;
		if (fish.transform.position.x < stopPoint.transform.position.x && timer < 5){
			fish.transform.Translate(Vector3.right * (sWidth * movementSpeed * Time.deltaTime));
		}
		if (fish.transform.position.x < endPoint.position.x && timer > 4){
			fish.transform.Translate(Vector3.right * (sWidth * movementSpeed * Time.deltaTime));
		}
		if (timer > 5){
			Destroy(gameObject);
		}

		if (fishReward is not null){
		}
	}
	
	private void OnSingleTapMessageReceived(SingleTapMessage obj){
		if (obj.TappedObject == "Bubble"){
			fish.GetComponent<Image>().CrossFadeAlpha(0,0,false);
			fishReward.SetActive(true);
			fishReward.GetComponent<Image>().CrossFadeAlpha(0, 1, false);
			StartCoroutine(DelayDestruction());
		}
	}

	private IEnumerator DelayDestruction(){
		yield return new WaitForSeconds(1);
		Destroy(gameObject);
	}
	private void OnDestroy(){
		Broker.Unsubscribe<SingleTapMessage>(OnSingleTapMessageReceived);
	}
}
