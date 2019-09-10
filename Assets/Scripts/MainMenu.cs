using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
        Destroy(gameObject, 3f);
    }
}
