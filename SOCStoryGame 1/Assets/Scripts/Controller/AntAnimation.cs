using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AntAnimation : MonoBehaviour {
	[SerializeField] private Sprite[] sprites;
	[SerializeField] private Image image;
	[SerializeField] private float animationDelay1, animationDelay2, animationDelay3;
	private int currentSprite;
	private bool canChangeSprite = true;

	private void Update(){
		image.sprite = sprites[currentSprite];
		if (canChangeSprite){
			StartCoroutine(ChangeSprite(animationDelay1));
		}
	}
	private IEnumerator ChangeSprite(float delay){
		canChangeSprite = false;
		yield return new WaitForSeconds(delay);
		currentSprite++;
		if (currentSprite == sprites.Length){
			currentSprite = 0;
		}
		canChangeSprite = true;
	}
}
