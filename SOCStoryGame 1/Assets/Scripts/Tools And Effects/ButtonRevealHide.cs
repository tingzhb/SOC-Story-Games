using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ButtonRevealHide : MonoBehaviour{ 
	[SerializeField] private GameObject close, open;
	[SerializeField] private string objectName;
	[SerializeField] private Sprite cardSprite, buttonSprite;
	[SerializeField] private float revealDelay = 0.15f, hideDelay = 0.125f;
	private GameObject closeInstance, openInstance;
	private bool hidden = true;
	private Image cardImage;

	private void Start(){
		cardImage = GetComponent<Image>();
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
		var parent = transform;
		openInstance = Instantiate(open, parent.position, Quaternion.identity, parent); 
		StartCoroutine(DelayReveal());
	}
	
	private IEnumerator DelayReveal(){
		yield return new WaitForSeconds(revealDelay);
		Destroy(openInstance);
	}

	private IEnumerator DelayHide(){
		yield return new WaitForSeconds(1f);
		var parent = transform;
		closeInstance = Instantiate(close, parent.position, Quaternion.identity, parent);
		yield return new WaitForSeconds(hideDelay);
		cardImage.sprite = buttonSprite;
		Destroy(closeInstance);
		GetComponent<Button>().interactable = true;
	}

	private void OnDestroy(){
		Broker.Unsubscribe<CorrectMessage>(OnCorrectMessageReceived);
		Broker.Unsubscribe<InvalidMessage>(OnInvalidMessageReceived);
	}
}
