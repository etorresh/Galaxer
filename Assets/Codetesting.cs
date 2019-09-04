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


        print(Nasty2D(Paths));
   
        //blacklist.Add(1);


        //Debug.Log(LowestValue(Paths[0, 0]));
        //Debug.Log(LowestValue(Paths[0, 0]));

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

    private string Nasty2D(List<int>[,] nastyArray)
    {
        string boardS = "";
        for (int i = 0; i < nastyArray.GetLength(0); i++)
        {
            for (int j = 0; j < nastyArray.GetLength(1); j++)
            {
                List<int> insideList = Paths[i, j];
                foreach (int k in insideList)
                {
                    print("siguiente letra");
                    boardS += k + "/";
                }
                boardS += "-";
                if (j == (nastyArray.GetLength(1) - 1))
                {
                    boardS += System.Environment.NewLine;
                }
            }
        }
        return boardS;
    }
}
