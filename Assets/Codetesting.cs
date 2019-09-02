using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Codetesting : MonoBehaviour
{
    private List<int>[,] Paths = new List<int>[8, 8];
    private List<int> blacklist = new List<int>();


    void Start()
    {
        for (int i = 0; i < Paths.GetLength(0); i++)
        {
            for (int j = 0; j < Paths.GetLength(1); j++)
            {
                Paths[i, j] = new List<int>();
            }
        }

        Paths[0, 0].Add(1);
        Paths[0, 0].Add(2);
        Paths[0, 0].Add(3);
        blacklist.Add(1);


        Debug.Log(LowestValue(Paths[0, 0]));
        Debug.Log(LowestValue(Paths[0, 0]));

    }

    private int LowestValue(List<int> x)
    {
        int minValue = 0;
        while(x.Any() && blacklist.Contains(x.Min()))
        {
            x.Remove(x.Min());
        }
        if(x.Any())
        {
            minValue = x.Min();
        }
        
        return minValue;
    }
}
