using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{

    public Transform camTarget;

    // Use this for initialization
    void Start()
    {
    }

    public void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, camTarget.position, .05f);
    }
}
