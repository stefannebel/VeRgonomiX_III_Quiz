using UnityEngine;
using UnityEngine.UIElements;

public class UICameraFacing : MonoBehaviour
{


    public Transform targetCamera; 
    public float distanceFromCamera = 5f;
    public float smoothSpeed = 10f;
    public Transform CameraFollower;

    private void Start()
    {
        Vector3 temp = CameraFollower.transform.position;
        temp.z = distanceFromCamera;
        CameraFollower.transform.position = temp;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, CameraFollower.position, smoothSpeed);
        transform.LookAt(targetCamera.position);
    }

    public void setDistance(float distance)
    {
        Vector3 temp = CameraFollower.transform.position;
        temp.z = distance;
        CameraFollower.transform.position = temp;
    }


}
