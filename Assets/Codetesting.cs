using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Codetesting : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(sayYeet());
    }

    IEnumerator sayYeet()
    {
        print("Waiting");
        yield return new WaitForSeconds(0.5f);
        print("yeet");
        sayYeet();
    }
}
