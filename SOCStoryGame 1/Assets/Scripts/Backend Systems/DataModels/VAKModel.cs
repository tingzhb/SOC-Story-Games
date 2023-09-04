using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "VAKModel")]
public class VAKModel : ScriptableObject{
	[SerializeField] float v, a, k;
	
	public float VValue{
		get => v;
		set => v = value;
	}

	public float AValue{
		get => a;
		set => a = value;
	}

	public float KValue{
		get => k;
		set => k = value;
	}
}
