using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fullscreenmenu : MonoBehaviour
{
    public void SetFullscreen (bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}
