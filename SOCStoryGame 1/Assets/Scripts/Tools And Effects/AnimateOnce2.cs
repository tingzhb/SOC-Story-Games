using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AnimateOnce2 : MonoBehaviour{
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
		}
		if (currentSprite == 20){
			EggMessage eggMessage = new(){
				Saved = true
			};
			Broker.InvokeSubscribers(typeof(EggMessage), eggMessage);
		}
		if (currentSprite == 29){
			EggMessage eggMessage = new(){
				Saved = false
			};
			Broker.InvokeSubscribers(typeof(EggMessage),eggMessage);
		}
	}

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
