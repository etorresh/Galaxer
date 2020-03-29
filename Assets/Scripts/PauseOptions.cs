using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseOptions : MonoBehaviour
{
    public Camera cameraMain;

    void Start()
    {
        Vector3 mPos = cameraMain.ViewportToWorldPoint(new Vector3(0.5f, 1, 0));
        mPos.z -= 0.5f;
        mPos.y = 0;
        transform.position = mPos;
    }
}
