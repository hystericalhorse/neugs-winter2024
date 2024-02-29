using UnityEngine;

public class ObjectiveHandler : MonoBehaviour
{
    [SerializeField] string objname;
    [SerializeField] string objdesc;

    public void SetObjectiveName(string name)
    {
        objname = name;
    }

    public void SetObjectiveDesc(string desc)
    {
        objdesc = desc;
    }

    [ContextMenu("Add Objective")]
    public void AddObjective()
    {
        PlayerManager.instance.objectiveList.AddObjective(objname, objdesc);
    }

    public void UpdateObjective()
    {
        PlayerManager.instance.objectiveList.UpdateObjective(objname, objdesc);
	}

    [ContextMenu("Resolve Objective")]
    public void ResolveObjective()
    {
        PlayerManager.instance.objectiveList.RemoveObjective(objname);
	}

    public void ResolveObjective(string name)
    {
		PlayerManager.instance.objectiveList.RemoveObjective(name);
	}
}
