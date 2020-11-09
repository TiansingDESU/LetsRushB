using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NetworkPrefab
{
    public GameObject Prefab;
    public string Path;

    public NetworkPrefab(GameObject go, string path)
    {
        Prefab = go;
        Path = ReturnPrefabPathModified(path);
    }

    //cut off From
    //Asset/Resources/File.prefab
    //to
    //Resources/File
    private string ReturnPrefabPathModified(string path)
    {
        int extensionLength = System.IO.Path.GetExtension(path).Length;
        int startIdx = path.ToLower().IndexOf("resources");

        if (startIdx == -1)
            return string.Empty;
        else
            return path.Substring(startIdx, path.Length - (startIdx + extensionLength));
    }
}
