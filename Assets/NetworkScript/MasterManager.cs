using Photon.Pun;
using Photon.Realtime;
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

    public static GameObject NetworkInstantiate(string prefabName, Vector3 pos, Quaternion rotation)
    {
        foreach (var prefab in instance.networkPrefabList)
        {
            if (prefab.Prefab.name == prefabName)
            {
                GameObject result = PhotonNetwork.Instantiate(prefab.Path, pos, rotation);
                return result;
            }
        }
        Debug.LogError("not found in networkPrefabList for gameobject name " + prefabName);
        return null;
    }

#if UNITY_EDITOR
    [RuntimeInitializeOnLoadMethod]
    private static void PopularNetworkPrefab()
    {
        if (!Application.isEditor)
            return;

        GameObject[] results = Resources.LoadAll<GameObject>("");
        instance.networkPrefabList.Clear();
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
#endif

    public static Player GetMyPlayerInfo()
    {
        return PhotonNetwork.LocalPlayer;
    }

    public static void NetworkDestroy(GameObject go)
    {
        PhotonNetwork.Destroy(go);
    }
}
