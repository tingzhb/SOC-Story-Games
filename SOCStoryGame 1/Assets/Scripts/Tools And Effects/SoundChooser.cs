using UnityEngine;

public class SoundChooser : MonoBehaviour {

	public void ChooseCow(){
		EggMessage eggMessage = new(){
			Saved = false
		};
		Broker.InvokeSubscribers(typeof(EggMessage), eggMessage);
		// Send sound message
	}
	
	public void ChooseCat(){
		EggMessage eggMessage = new(){
			Saved = true
		};
		Broker.InvokeSubscribers(typeof(EggMessage), eggMessage);
		// Send sound message
	}
}
