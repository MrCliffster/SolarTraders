using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseSetupOnClick : MonoBehaviour
{
    public GameObject panel;
    public void CloseSetup()
    {
        panel.SetActive(false);
    }
}
