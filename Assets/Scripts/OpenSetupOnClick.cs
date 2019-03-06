using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenSetupOnClick : MonoBehaviour
{
    public GameObject panel;
    public void OpenSetup()
    {
        panel.SetActive(true);
    }
}
