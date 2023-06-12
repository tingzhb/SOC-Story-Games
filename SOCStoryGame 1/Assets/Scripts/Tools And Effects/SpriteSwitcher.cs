using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SpriteSwitcher : MonoBehaviour{
	[SerializeField] private Sprite[] sprites;
	[SerializeField] private Image image;
	[SerializeField] private float animationDelay;
	private int currentSprite;
	private float timePassed;

	private void Update(){
		timePassed += Time.deltaTime;
		image.sprite = sprites[currentSprite];
		if (timePassed >= animationDelay){
			Change();
		}
	}

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
