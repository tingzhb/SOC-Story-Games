using UnityEngine;

public class C7Chooser : MonoBehaviour {
	[SerializeField] private GameObject choice, cow, cat;
	
	public void ChooseCow(){
		choice.SetActive(true);
		cow.SetActive(true);
	}
	public void ChooseCat(){
		choice.SetActive(true);
		cat.SetActive(true);
	}
}
