using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AnimateOnce : MonoBehaviour{
	[SerializeField] private Sprite[] sprites;
	[SerializeField] private Image image;
	[SerializeField] private float animationDelay;
	[SerializeField] private bool startOnAwake;
	private bool canChangeSprite;
	private int currentSprite;
	public bool canAnimate = true;
	private float timePassed;

	private void Awake(){
		if (startOnAwake){
			StartAnimation();
		}
	}
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
	// 	canChangeSprite = true;
	// 	if (currentSprite == sprites.Length){
	// 		currentSprite = sprites.Length - 1;
	// 		canChangeSprite = false;
	// 	}
	// }
	private void Change(){
		currentSprite++;
		timePassed = 0;
		if (currentSprite == sprites.Length){
			currentSprite = sprites.Length - 1;
			canChangeSprite = false;
		}
	}
	public void StartAnimation(){
		if (canAnimate){
			currentSprite = 0;
			canChangeSprite = true;
		}
	}
}
