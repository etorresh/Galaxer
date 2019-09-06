using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Codetesting : MonoBehaviour
{
    int[,] board;

    void Start()
    {
        board = new int[8, 8];
        board[1, 1] = 3;
        board[2, 1] = 3;

        int startingX = 1;
        int startingY = 1;

        StartCoroutine(Point(true));
    }

    IEnumerator Point(bool x)
    {
        Debug.Log("starting");
        yield return new WaitForSeconds(1f);
        Debug.Log("calling itself");
        if(x)
        {
            yield return StartCoroutine(Point(false));
        }
        Debug.Log("ending");
    }
}
