using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Image title;
    public Image background;

    void Start()
    {
        Invoke("Dissapear", 1.5f);
    }

    void Dissapear()
    {
        title.CrossFadeAlpha(0, 3, false);
        background.CrossFadeAlpha(0, 3, false);
        Destroy(gameObject, 3f);
    }
}
