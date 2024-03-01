using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class PathingMonster : MonoBehaviour
{
    [SerializeField] List<Transform> path = new();
	[SerializeField] bool isStatic = false;
    [SerializeField]
    float moveSpeed = 1;
    [SerializeField] float minPatrolTime = 0;
    [SerializeField] float maxPatrolTime = 1;

	int nextNodeIndex;
    float patrolTimer;

    void Start()
    {
        if (path.Count == 0)
        {
            isStatic = true;
		}
        else
        {
            nextNodeIndex = (path.Count > 1) ? 1 : 0;
		}
	}

    
    void Update()
    {
        if (isStatic) return;

        if (Vector2.Distance(transform.position, path[nextNodeIndex].position) > 0.1f)
        {
            transform.position = (Vector3)Vector2.Lerp(transform.position, path[nextNodeIndex].position, Time.deltaTime * moveSpeed);
            if (Vector2.Distance(transform.position, path[nextNodeIndex].position) <= 0.1f)
                patrolTimer = Random.Range(minPatrolTime, maxPatrolTime);
        }
        else
        {
            patrolTimer -= Time.deltaTime;
            if (patrolTimer <= 0)
                nextNodeIndex = (nextNodeIndex + 1 >= path.Count) ? 0 : nextNodeIndex + 1;
        }
    }
}
