using UnityEngine;

public class C7Chooser : MonoBehaviour {
	[SerializeField] private GameObject choice, crab, cow, cat;

	public void ChooseCrab(){
		choice.SetActive(true);
		crab.SetActive(true);
	}
	public void ChooseCow(){
		choice.SetActive(true);
		cow.SetActive(true);
	}
	public void ChooseCat(){
		choice.SetActive(true);
		cat.SetActive(true);
	}
}
