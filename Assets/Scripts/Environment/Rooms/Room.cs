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

    public void Awake()
	{
        roomCenter = (Vector2) transform.position;
        onEnterRoom?.AddListener(SetCameraControllerCenter);
	}

    public void OnEnterRoom() => onEnterRoom?.Invoke();
    public void OnExitRoom() => onExitRoom?.Invoke();

    protected void SetCameraControllerCenter()
    {
        var cam = PlayerManager.instance?.GetCameraController();
        if (cam)
        {
			cam.Center = roomCenter;
			cam.Limits = RoomBounds;
			cam.ResetController();
		}
    }

#if UNITY_EDITOR
	void OnEnable()
	{
        SceneView.duringSceneGui += OnScene;
	}

	void OnDisable()
	{
		SceneView.duringSceneGui -= OnScene;

        onEnterRoom?.RemoveAllListeners();
        onExitRoom?.RemoveAllListeners();
	}

	private void OnScene(SceneView scene)
    {
        roomCenter = (Vector2) transform.position;
    }

	private void OnDrawGizmos()
	{
        Gizmos.color = boundsCrossColor;
        var halfBounds = RoomBounds * 0.5f;

        Vector2 tL = new(roomCenter.x-halfBounds.x,roomCenter.y-halfBounds.y);
        Vector2 bL = new(roomCenter.x-halfBounds.x,roomCenter.y+halfBounds.y);
        Vector2 tR = new(roomCenter.x+halfBounds.x,roomCenter.y-halfBounds.y);
        Vector2 bR = new(roomCenter.x+halfBounds.x,roomCenter.y+halfBounds.y);

        Gizmos.DrawLine(tL,tR);
        Gizmos.DrawLine(tR,bR);
        Gizmos.DrawLine(bR,bL);
        Gizmos.DrawLine(bR,bL);
        Gizmos.DrawLine(bL,tL);
	}
#endif
}

