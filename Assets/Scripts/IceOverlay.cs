using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceOverlay : MonoBehaviour
{
    public GameObject iceOverlayUI;

    public void ShowIceOverlay(bool active)
    {
        iceOverlayUI.SetActive(active);
    }
}
