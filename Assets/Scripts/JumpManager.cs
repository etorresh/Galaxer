using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpManager : MonoBehaviour
{
    private GameObject jumpAnimation;
    public GameObject jumpAnimation1, jumpAnimation2, jumpAnimation3, jumpAnimation4;

    private Turns turns;

    private void Start()
    {
        turns = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Turns>();
    }

    public void Jump(int positionX, int positionZ)
    {
        ChooseModel();
        Vector3 originPos = new Vector3(positionX, 0.1f, -positionZ + 4);
        if (positionX != 7)
        {
            Instantiate(jumpAnimation, originPos, Quaternion.identity);
        }
        if (positionZ != 7)
        {
            Instantiate(jumpAnimation, originPos, Quaternion.identity).transform.Rotate(0, 90, 0);
        }
        if (positionX != 0)
        {
            Instantiate(jumpAnimation, originPos, Quaternion.identity).transform.Rotate(0, 180, 0);
        }
        if (positionZ != 0)
        {
            Instantiate(jumpAnimation, originPos, Quaternion.identity).transform.Rotate(0, 270, 0);
        }
    }

    private void ChooseModel()
    {
        switch (turns.currentTurn)
        {
            case 1:
                jumpAnimation = jumpAnimation1;
                break;
            case 2:
                jumpAnimation = jumpAnimation2;
                break;
            case 3:
                jumpAnimation = jumpAnimation3;
                break;
            case 4:
                jumpAnimation = jumpAnimation4;
                break;
        }
    }
}
