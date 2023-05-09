using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] 
    private float keyboardSensitivity;

    [SerializeField]
    private float mouseSensitivity;

    [SerializeField]
    private float smoothTime;

    [SerializeField]
    private float distanceFromTarget;

    [SerializeField]
    private float minimumDistanceFromTarget;

    [SerializeField]
    private float maximumDistanceFromTarget;

    [SerializeField] 
    private Transform target;

    [SerializeField]
    private float zoomSpeed;

    private float rotationX;
    private float rotationY;
    private float deltaZ;

    private Camera cam;
    private Vector3 currentRotation;
    private Vector3 smoothVelocity = Vector3.zero;

    private void Awake() => cam = Camera.main;

    private void Update()
    {
        RotateCamera();
        AdjustZoom();
    }

    private void RotateCamera()
    {
        float mouseX = 0f;
        float mouseY = 0f;

        if(Input.GetMouseButtonDown(1) || Input.GetMouseButton(1) || Input.GetMouseButtonUp(1)){
            mouseX = Input.GetAxis("Mouse X");
            mouseY = -Input.GetAxis("Mouse Y");
        }

        var keyX = -Input.GetAxis("Horizontal");
        var keyY = Input.GetAxis("Vertical");

        var deltaX = (mouseX * mouseSensitivity) + (keyX * keyboardSensitivity);
        var deltaY = (mouseY * mouseSensitivity) + (keyY * keyboardSensitivity);

        rotationX += deltaY;
        rotationY += deltaX;

        rotationX = Mathf.Clamp(rotationX,-10f,75f);

        Vector3 nextRotation = new Vector3(rotationX, rotationY);
        currentRotation = Vector3.SmoothDamp(currentRotation, nextRotation, ref smoothVelocity, smoothTime);
        cam.transform.localEulerAngles = currentRotation;

        cam.transform.position = target.position - cam.transform.forward * distanceFromTarget;
    }

    private void AdjustZoom(){
        deltaZ = -Input.mouseScrollDelta.y;
        float nextDistanceFromTarget = Mathf.Clamp((distanceFromTarget + deltaZ),minimumDistanceFromTarget,maximumDistanceFromTarget);
        float currentDistanceFromTarget = distanceFromTarget;
        distanceFromTarget = Mathf.Lerp(currentDistanceFromTarget,nextDistanceFromTarget,zoomSpeed);
    }

}
