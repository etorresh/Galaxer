using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Codetesting : MonoBehaviour
{
    void Start()
    {
        int currentPosition = 2;
        int[] surroundingValues = new int[4];
        surroundingValues[0] = 4;
        surroundingValues[1] = 6;
        surroundingValues[2] = 5;
        surroundingValues[3] = 3;

        Array.Sort(surroundingValues);
        int sIndex = Array.BinarySearch(surroundingValues, currentPosition);
        print(~sIndex);
        Debug.Log(surroundingValues[~sIndex]);

    }
}
