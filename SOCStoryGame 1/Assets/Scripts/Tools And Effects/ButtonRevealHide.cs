using System.Collections;
using UnityEngine;

public class ButtonRevealHide : MonoBehaviour{ 
	[SerializeField] private GameObject button, img, close, open;
	[SerializeField] private string objectName;
	private bool hidden = true;
	private AnimateOnce animateOpen;
	private AnimateOnce animateClose;

	private void Start(){
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
		GetComponent<UnityEngine.UI.Button>().interactable = false;
	}

	private void Hide(){
		GetComponent<UnityEngine.UI.Button>().interactable = true;
		close.SetActive(true);
		animateClose.canAnimate = true;
		animateClose.StartAnimation();
		StartCoroutine(DelayHide());
	}

	public void Reveal(){
		hidden = false;
		CardMessage cardMessage = new(){
			CardName = objectName
		};
		Broker.InvokeSubscribers(typeof(CardMessage), cardMessage);
		LockButton(); 
		button.SetActive(false);
		img.SetActive(true);
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
		img.SetActive(false);
		button.SetActive(true);
		animateClose.canAnimate = false;
		close.SetActive(false);
	}
}
