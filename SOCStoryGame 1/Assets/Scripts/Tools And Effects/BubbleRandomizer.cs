using System.Collections;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class BubbleRandomizer : MonoBehaviour{
	[SerializeField] private char[] characters;
	[SerializeField] private int movementSpeed;
	private TextMeshProUGUI textComp;


	private void Start(){
		textComp = GetComponentInChildren<TextMeshProUGUI>();
		var randomChar = Random.Range(0, characters.Length);
		textComp.text = characters[randomChar].ToString();
		if (randomChar == 1){
			gameObject.tag = "Bubble";
		}
	}
	private void FixedUpdate() {
		transform.Translate(Vector3.up * (Time.deltaTime * (movementSpeed + RandomDisplacement())));
		transform.Translate(Vector3.left * (Time.deltaTime * (movementSpeed + RandomDisplacement())));
	}
	private int RandomDisplacement() {
		return Random.Range(-movementSpeed/2, movementSpeed/2);
	}
	public void Pop() {
		if (gameObject.CompareTag("Bubble")) {
			GetComponent<AnimateOnce>().StartAnimation();
			StartCoroutine(DelayDestroy());
		}
	}
	private IEnumerator DelayDestroy() {
		yield return new WaitForSeconds(0.25f);
		textComp.text = " ";
	}
}
