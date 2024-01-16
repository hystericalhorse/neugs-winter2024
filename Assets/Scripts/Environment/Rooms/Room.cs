using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Room : MonoBehaviour
{
    public string RoomName;

    [SerializeField] UnityEvent onEnterRoom;
    [SerializeField] UnityEvent onExitRoom;

    private Vector2 roomCenter;
    public Vector2 RoomBounds = Vector2.positiveInfinity;

	public void Start()
	{
        roomCenter = (Vector2) transform.position;
        onEnterRoom.AddListener(SetCameraControllerCenter);
	}

    public void OnEnterRoom() => onEnterRoom?.Invoke();
    public void OnExitRoom() => onExitRoom?.Invoke();

    protected void SetCameraControllerCenter()
    {
        var cam = GameObject.FindObjectOfType<CameraController>();
        cam.Limits = roomCenter + RoomBounds;
    }
}
