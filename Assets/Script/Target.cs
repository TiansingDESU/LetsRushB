using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private static List<Target> targetList;

    public static Target getCloest(Vector3 Pos, float maxRange)
    {
        Target closest = null;
        foreach(Target target in targetList)
        {
            float posToTarget = Vector3.Distance(Pos, target.GetPosition());
            if (posToTarget <= maxRange)
            {
                if (closest == null)
                    closest = target;
                else if (posToTarget < Vector3.Distance(Pos, closest.GetPosition()))
                {
                    closest = target;
                }
            }
        }
        return closest;
    }

    private void Awake()
    {
        if (targetList == null)
            targetList = new List<Target>();
        targetList.Add(this);
    }

    public void Damage()
    {
        //Debug.Log("Hit!");
        if (this.GetComponent<Animation>() != null)
        {
            this.GetComponent<Animation>().Play();
        }
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }
}
