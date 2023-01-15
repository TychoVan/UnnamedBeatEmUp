using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Winscreenactive : MonoBehaviour
{
    public GameObject winscreen;

    public void WinActive()
    {
        winscreen.SetActive(true);
    }
}
