using UnityEngine;

public class Pause : MonoBehaviour
{
    public Camera cameraMain;
    public GameObject pauseOptions;
    public Turns turns;

    void Start()
    {
        Vector3 mPos = cameraMain.ViewportToWorldPoint(new Vector3(0.5f, 1, 0));
        mPos.z -= gameObject.GetComponent<Collider>().bounds.size.z / 2;
        mPos.y = 0;
        transform.position = mPos;
    }

    private void OnMouseDown()
    {
        turns.IsClickable = false;
        pauseOptions.SetActive(true);
        gameObject.SetActive(false);
    }
}
