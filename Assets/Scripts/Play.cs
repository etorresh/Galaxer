using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Play : MonoBehaviour
{
    private Turns turns;

    private void OnMouseDown()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Spawner");
        foreach (GameObject obj in objects)
        {
            obj.GetComponent<PlayerSpawn>().StartGame();
        }
        turns = GameObject.FindWithTag("MainCamera").GetComponent<Turns>();
        turns.GameStart();
        Destroy(gameObject);
    }
}
