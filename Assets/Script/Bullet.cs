using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviourPun
{

    public float moveSpeed = 10;
    public float hitDetectedSize = 1f;
    public float Damage = 24f;

    Vector3 shootDir;

    [Header("Ray探测")]
    public Transform startPos;
    public Transform endPos;
    public LayerMask bulletHitMask;

    [Header("特效")]
    public GameObject FX_PlayerHit;
    public GameObject FX_SolidHit;

    private Vector3 Dir;
    private float Length;

    Action destory;

    private void Start()
    {
        Dir = Vector3.Normalize(endPos.position - startPos.position);
        Length = Vector3.Distance(startPos.position, endPos.position);
    }

    public void Setup(Vector3 shootDir)
    {
        this.shootDir = shootDir;
        destory = delegate { PhotonNetwork.Destroy(gameObject); };
        TimeDelay.SetTimeout(destory, 5f);
    }

    private void OnDestroy()
    {
        destory = null;
    }

    private void Update()
    {
        transform.position += shootDir * Time.deltaTime * moveSpeed;

        #region Hit Function
        //针对地图小物件的击中判断,只判断距离，不判断boxCollider
        Target target = Target.getCloest(transform.position, hitDetectedSize);
        if (target != null)
        {
            target.Damage();
            Destroy(gameObject);
        }

        //针对需要物理探测Collider的方式
        RaycastHit hit;
        if(Physics.Raycast(startPos.position, Dir, out hit, Length, bulletHitMask))
        {
            if (hit.collider.GetComponent<HitBox>() != null)
            {
                Transform targetPlayer = hit.collider.GetComponent<HitBox>().Player;
                targetPlayer.GetComponent<PlayerActionEvent>().OnHit?.Invoke(Damage);
                if (FX_PlayerHit != null)
                {
                    GameObject go = GameObject.Instantiate(FX_PlayerHit);
                    go.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    go.transform.position = (transform.position + hit.point) / 2;
                    go.transform.rotation = transform.rotation;
                }
            }
            else
            {
                if (FX_SolidHit != null)
                {
                    GameObject go = GameObject.Instantiate(FX_SolidHit);
                    go.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    go.transform.position = (transform.position + hit.point) / 2;
                    go.transform.rotation = transform.rotation;
                }
            }
            Destroy(gameObject);
        }
        #endregion

    }
}
