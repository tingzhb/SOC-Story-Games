using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ButtonRevealHide : MonoBehaviour{ 
	[SerializeField] private GameObject button, img, close, open;
	[SerializeField] private string objectName;
	[SerializeField] private Sprite cardSprite, buttonSprite;
	private bool hidden = true;
	private AnimateOnce animateOpen, animateClose;
	private Image cardImage;

	private void Start(){
		cardImage = GetComponent<Image>();
		animateClose = close.GetComponent<AnimateOnce>();
		animateOpen = open.GetComponent<AnimateOnce>();
		Broker.Subscribe<CorrectMessage>(OnCorrectMessageReceived);
		Broker.Subscribe<InvalidMessage>(OnInvalidMessageReceived);
	}
	private void OnCorrectMessageReceived(CorrectMessage obj){
		if (obj.Name == objectName){
			LockButton();
		}
	}

	private void OnInvalidMessageReceived(InvalidMessage obj){
		if (obj.Type == objectName && !hidden){
			Hide();
		}
	}

	private void LockButton(){
		GetComponent<Button>().interactable = false;
	}

	private void Hide(){
		GetComponent<Button>().interactable = true;
		close.SetActive(true);
		animateClose.canAnimate = true;
		animateClose.StartAnimation();
		StartCoroutine(DelayHide());
		hidden = true;
	}

	public void Reveal(){
		hidden = false;
		CardMessage cardMessage = new(){
			CardName = objectName
		};
		Broker.InvokeSubscribers(typeof(CardMessage), cardMessage);
		LockButton(); 
		cardImage.sprite = cardSprite;
		open.SetActive(true);
		animateOpen.canAnimate = true;
		animateOpen.StartAnimation();
		StartCoroutine(DelayReveal());
	}
	
	private IEnumerator DelayReveal(){
		yield return new WaitForSeconds(0.5f);
		animateOpen.canAnimate = false;
		open.SetActive(false);
	}
	
	private IEnumerator DelayHide(){
		yield return new WaitForSeconds(0.5f);
		cardImage.sprite = buttonSprite;
		animateClose.canAnimate = false;
		close.SetActive(false);
	}
}
