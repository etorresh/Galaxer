using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Play : MonoBehaviour
{
    public Turns turns;
    public PlayerSpawn spawner1, spawner2, spawner3, spawner4;
    public AdManager adM;


    private void OnMouseDown()
    {
        spawner1.StartGame();
        spawner2.StartGame();
        spawner3.StartGame();
        spawner4.StartGame();
        turns.GameStart();
        adM.StartCoroutine(adM.ShowBannerWhenReady());
        Destroy(gameObject);
    }
}
