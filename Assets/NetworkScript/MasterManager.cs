using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MasterManager : MonoBehaviourPunCallbacks
{
    public static MasterManager instance;

    private void Awake()
    {
        instance = this;
    }

    public List<NetworkPrefab> networkPrefabList = new List<NetworkPrefab>();

    public static GameObject NetworkInstantiate(GameObject go, Vector3 pos, Quaternion rotation)
    {
        foreach (var prefab in instance.networkPrefabList)
        {
            if (prefab.Prefab == go)
            {
                GameObject result = PhotonNetwork.Instantiate(prefab.Path, pos, rotation);
                return result;
            }
            else
            {
                Debug.LogError("Path is empty for gameobject name " + prefab.Prefab);
                return null;
            }
        }
        return null;
    }

    [RuntimeInitializeOnLoadMethod]
    private static void PopularNetworkPrefab()
    {
        if (!Application.isEditor)
            return;

        GameObject[] results = Resources.LoadAll<GameObject>("");
        for (int i = 0; i < results.Length; i++)
        {
            if (results[i].GetComponent<PhotonView>() != null)
            {
                string path = AssetDatabase.GetAssetPath(results[i]);
                if(path.Contains("NetworkPrefabs"))
                    instance.networkPrefabList.Add(new NetworkPrefab(results[i], path));
            }
        }
    }
}
