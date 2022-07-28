using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SpriteSwitcher : MonoBehaviour{
	[SerializeField] private Sprite[] sprites;
	[SerializeField] private Image image;
	[SerializeField] private float animationDelay;
	private bool canChangeSprite = true;
	private int currentSprite;
	private float timePassed;

	private void Update(){
		timePassed += Time.deltaTime;
		image.sprite = sprites[currentSprite];
		if (canChangeSprite && timePassed >= animationDelay){
			Change();
			// StartCoroutine(ChangeSprite());
		}
	}
	// private IEnumerator ChangeSprite(){
	// 	canChangeSprite = false;
	// 	yield return new WaitForSeconds(animationDelay);
	// 	currentSprite++;
	// 	if (currentSprite == sprites.Length){
	// 		currentSprite = 0;
	// 	}
	// 	canChangeSprite = true;
	// }
	
	private void Change(){
		currentSprite++;
		timePassed = 0;
		if (currentSprite == sprites.Length){
			currentSprite = 0;
		}
	}
	private void OnDisable(){
		currentSprite = 0;
	}
}
