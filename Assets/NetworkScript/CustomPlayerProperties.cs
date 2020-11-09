using Assets;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomPlayerProperties : MonoBehaviourPunCallbacks
{

    public static CustomPlayerProperties instance;

    private void Awake()
    {
        instance = this;
    }

    public EZAction<Player, ExitGames.Client.Photon.Hashtable> OnPlayerUpdateProperties = new EZAction<Player, ExitGames.Client.Photon.Hashtable>();

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

    public override void OnPlayerPropertiesUpdate(Player target, ExitGames.Client.Photon.Hashtable changedProps)
    {
        base.OnPlayerPropertiesUpdate(target, changedProps);
        if (target != null)
        {
            OnPlayerUpdateProperties?.Invoke(target, changedProps);
        }
    }

}
