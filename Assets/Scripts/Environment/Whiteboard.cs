public class Whiteboard : CutscenePlayer
{
	public override void OnInteract()
	{
		var combination = FindAnyObjectByType<LockedBox>().GetComboVec3();
		Shot shot = new Shot();
		shot.shotScript.Add(combination.y.ToString() + "-" + combination.x.ToString() + "-" + combination.z.ToString());

		cutscene.Add(shot);

		base.OnInteract();
	}
}
