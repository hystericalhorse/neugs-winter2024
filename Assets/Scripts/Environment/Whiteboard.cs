public class Whiteboard : CutscenePlayer
{
	public override void OnInteract()
	{
		var combination = FindAnyObjectByType<LockedBox>().GetComboVec3();
		cutscene.Clear();

		Shot shot = new Shot();
		shot.shotScript.Add(combination.x.ToString() + "-" + combination.y.ToString() + "-" + combination.z.ToString());

		cutscene.Add(shot);

		base.OnInteract();
	}
}
