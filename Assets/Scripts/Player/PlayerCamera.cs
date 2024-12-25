using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public static readonly float BASE_CAMERA_SPEED_DEGREES = 1000f;
    public static readonly float CAM_ROTATION_RADIUS = 1f;


    private float mHorizViewAngle = 0f;
    private float mVerticalViewAngle = 0f;

    [SerializeField] private GameObject mCameraObj;
    [SerializeField] public GameObject CloseReticle;
    [SerializeField] public GameObject FarReticle;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    private void Update()
    {
        mHorizViewAngle += Input.GetAxis("Mouse X") * BASE_CAMERA_SPEED_DEGREES * Time.deltaTime;
        mVerticalViewAngle -= Input.GetAxis("Mouse Y") * BASE_CAMERA_SPEED_DEGREES * Time.deltaTime;

        Quaternion rot = Quaternion.Euler(mVerticalViewAngle, mHorizViewAngle, 0);

        mCameraObj.transform.rotation = rot;
        mCameraObj.transform.position = transform.position + mCameraObj.transform.forward * CAM_ROTATION_RADIUS;

        FarReticle.transform.rotation = rot;

        //CloseReticle.transform.LookAt(mCameraObj.transform.position);
        //FarReticle.transform.LookAt(mCameraObj.transform.position);
        //CloseReticle.transform.Rotate(0, 0, 45);
    }

    public Quaternion GetViewDirection()
    {
        return Quaternion.LookRotation(mCameraObj.transform.forward, mCameraObj.transform.up);
    }
}
