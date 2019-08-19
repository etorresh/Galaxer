using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPos : MonoBehaviour
{
    public Collider playArea;

    void Awake()
    {
        float orthoSize = playArea.bounds.size.x * Screen.height / Screen.width * 0.5f;

        Camera.main.orthographicSize = orthoSize;
    }
}