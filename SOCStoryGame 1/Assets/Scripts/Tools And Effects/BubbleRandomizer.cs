using System;
using System.Collections;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class BubbleRandomizer : MonoBehaviour{
	[SerializeField] private char[] characters;
	[SerializeField] private float movementSpeed;
	private TextMeshProUGUI textComp;
	private float height;

	private void Awake(){
		height = Screen.height;
	}
	private void Start(){
		textComp = GetComponentInChildren<TextMeshProUGUI>();
		var randomChar = Random.Range(0, characters.Length);
		textComp.text = characters[randomChar].ToString();
		if (randomChar == 1){
			gameObject.tag = "Bubble";
		}
	}
	private void FixedUpdate() {
		transform.Translate(Vector3.up * (Time.deltaTime * (movementSpeed + RandomDisplacement()) * height));
		transform.Translate(Vector3.left * (Time.deltaTime * (movementSpeed + RandomDisplacement()) * height));
	}
	private int RandomDisplacement() {
		return Random.Range(-(int)movementSpeed/2, (int)movementSpeed/2);
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
