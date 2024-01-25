using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

[ExecuteInEditMode]
public class Room : MonoBehaviour
{
    public string RoomName;

    [SerializeField] UnityEvent onEnterRoom;
    [SerializeField] UnityEvent onExitRoom;
    [Space]
    [SerializeField] Color boundsCrossColor = Color.red;

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
        var cam = PlayerManager.instance.GetCameraController();
        cam.Center = roomCenter;
        cam.Limits = RoomBounds;
    }

#if UNITY_EDITOR
	void OnEnable()
	{
        SceneView.duringSceneGui += OnScene;
	}

	void OnDisable()
	{
		SceneView.duringSceneGui -= OnScene;

        onEnterRoom.RemoveAllListeners();
        onExitRoom.RemoveAllListeners();
	}

	private void OnScene(SceneView scene)
    {
        roomCenter = (Vector2) transform.position;
    }

	private void OnDrawGizmos()
	{
        Gizmos.color = boundsCrossColor;
		Gizmos.DrawLine(roomCenter - (RoomBounds*0.5f), roomCenter + (RoomBounds * 0.5f));
	}
#endif
}

