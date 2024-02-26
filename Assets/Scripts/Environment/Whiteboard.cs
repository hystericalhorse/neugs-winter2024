using TMPro;
using UnityEngine;

public class Whiteboard : CutscenePlayer
{
	[SerializeField] private GameObject CodeBoxes;
	private GameObject go;

	public void OnDestroy()
	{
		Destroy(go);
	}

	public override void OnInteract()
	{
		var combination = FindAnyObjectByType<LockedBox>().GetComboVec3();
		
		if (go == null)
		{
			go ??= Instantiate(CodeBoxes);
			go.transform.SetParent(FindObjectOfType<Canvas>().transform, false);

			var tb = go.GetComponent<TextboxGroup>();

			tb.textBoxes[0].text = combination.x.ToString();
			tb.textBoxes[1].text = combination.y.ToString();
			tb.textBoxes[2].text = combination.z.ToString();
		}
		else
		{
			go.SetActive(true);
		}

		base.OnInteract();
	}

	public void ToggleActive(bool activate) => go.SetActive(activate);

}