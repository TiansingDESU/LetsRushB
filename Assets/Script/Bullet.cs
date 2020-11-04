using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float moveSpeed = 10;
    public float hitDetectedSize = 1f;

    Vector3 shootDir;

    public void Setup(Vector3 shootDir)
    {
        this.shootDir = shootDir;
        Debug.Log(shootDir);
        transform.rotation = Quaternion.LookRotation(shootDir);
        Destroy(gameObject, 5f);
    }

    private void Update()
    {
        transform.position += shootDir * Time.deltaTime * moveSpeed;

        //针对地图小物件的击中判断,只判断距离，不判断boxCollider
        Target target = Target.getCloest(transform.position, hitDetectedSize);
        if (target != null)
        {
            target.Damage();
            Destroy(gameObject);
        }
    }
}
