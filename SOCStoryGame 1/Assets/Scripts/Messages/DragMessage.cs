using UnityEngine;

public class DragMessage : IMessage{
	public GameObject DragObject { get; set; }
	public bool Dragging { get; set; }
	public string ItemName { get; set; }
}