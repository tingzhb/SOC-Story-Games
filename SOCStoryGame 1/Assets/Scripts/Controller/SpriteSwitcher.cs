using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SpriteSwitcher : MonoBehaviour{
	[SerializeField] private Sprite[] sprites;
	[SerializeField] private Image image;
	[SerializeField] private float animationDelay;
	private int currentSprite;
	private bool canChangeSprite = true;
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
		if (currentSprite == sprites.Length){
			currentSprite = 0;
		}
		canChangeSprite = true;
	}
}
