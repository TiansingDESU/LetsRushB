using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomPlayerProperties : MonoBehaviour
{

    public static CustomPlayerProperties instance;

    private void Awake()
    {
        instance = this;
    }

    private ExitGames.Client.Photon.Hashtable customPlayerProperties = new ExitGames.Client.Photon.Hashtable();

    public void SetProperties(string key, int value)
    {
        customPlayerProperties[key] = value;
        PhotonNetwork.LocalPlayer.CustomProperties = customPlayerProperties;
    }

    public void RemoveProperties(string key)
    {
        customPlayerProperties.Remove(key);
        PhotonNetwork.LocalPlayer.CustomProperties = customPlayerProperties;
    }

    public bool TryGetProperties(string key, out System.Object value)
    {
        if (customPlayerProperties.ContainsKey(key))
        {
            value = customPlayerProperties[key];
            return true;
        }
        else
        {
            value = null;
            return false;
        }
    }

}
