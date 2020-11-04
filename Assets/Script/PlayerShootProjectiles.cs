using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerActionEvent))]
public class PlayerShootProjectiles : MonoBehaviour
{
    [SerializeField] 
    private Transform pfBullet;

    private void Awake()
    {
        this.GetComponent<PlayerActionEvent>().Shoot += OnShoot;
    }

    private void OnShoot(Vector3 gunEndPos, Vector3 shootDir)
    {
        //shoot
        Transform bulletTrans = Instantiate(pfBullet, gunEndPos, Quaternion.identity);
        bulletTrans.GetComponent<Bullet>().Setup(shootDir);
    }
}
