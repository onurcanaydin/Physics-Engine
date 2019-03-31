using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;

    private Vector3 cameraOffset;
    
    void Start()
    {
        cameraOffset = transform.position - player.transform.position;
    }
    
    void LateUpdate()
    {
        transform.position = player.transform.position + cameraOffset;
    }
}