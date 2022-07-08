using UnityEngine;

public class DragMessage : IMessage{
	public GameObject DragObject { get; set; }
	public bool CanDrag { get; set; }
}