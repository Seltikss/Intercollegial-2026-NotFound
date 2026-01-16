using UnityEngine;

public class CameraBounding : MonoBehaviour
{
    public Transform chamberTr;
    private Camera cam;
    private void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("asdf");
        if (collision.CompareTag("Player"))
            cam.transform.position = chamberTr.position + Vector3.back * 5;
    }
}
