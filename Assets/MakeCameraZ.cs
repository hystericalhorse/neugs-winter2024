using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeCameraZ : MonoBehaviour
{
    Vector3 pos = Vector3.zero;

	private void Start()
	{
		pos = transform.position;
	}

	// Update is called once per frame
	void Update()
    {
        pos.z = PlayerManager.instance.GetCameraController().transform.position.z;
		transform.SetPositionAndRotation(pos, transform.rotation);
    }
}
