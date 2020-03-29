using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unpause : MonoBehaviour
{
    public Turns turns;
    public GameObject pauseOptions;
    public GameObject pause;
    private void OnMouseDown()
    {
        turns.IsClickable = true;
        pause.SetActive(true);
        pauseOptions.SetActive(false);
    }
}
