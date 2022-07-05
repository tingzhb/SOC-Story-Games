using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AnimateOnce : MonoBehaviour{
	[SerializeField] private Sprite[] sprites;
	[SerializeField] private Image image;
	[SerializeField] private float animationDelay; 
	private bool canChangeSprite;
	private int currentSprite;
	private void Update(){
		image.sprite = sprites[currentSprite];
		if (canChangeSprite){
			StartCoroutine(ChangeSprite());
		}
	}
	private IEnumerator ChangeSprite(){
		canChangeSprite = false;
		yield return new WaitForSeconds(animationDelay);
		currentSprite++;
		canChangeSprite = true;
		if (currentSprite == sprites.Length){
			currentSprite = 0;
			canChangeSprite = false;
		}
	}
	public void StartAnimation(){
		canChangeSprite = true;
	}
}
