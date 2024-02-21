using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptDestroyer : MonoBehaviour
{
	public void Destroy<T>(T script) where T : MonoBehaviour
	{
		Destroy(script);
	}
}
