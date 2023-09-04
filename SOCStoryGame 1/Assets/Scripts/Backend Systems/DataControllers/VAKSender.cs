using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VAKSender : MonoBehaviour{
	[SerializeField] float v, a, k;

	public void SendVAK(){
		VAKMessage vakMessage = new() {
			V = v,
			A = a,
			K = k
		};
		Broker.InvokeSubscribers(typeof(VAKMessage), vakMessage);
	}
}
