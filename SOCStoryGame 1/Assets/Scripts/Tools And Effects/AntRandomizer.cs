using UnityEngine;
using Random = UnityEngine.Random;

public class AntRandomizer : MonoBehaviour {
	private void Awake(){
		var randomX = Random.Range(-570, 500);
		transform.Translate(randomX, 0, 0);
	}
}
