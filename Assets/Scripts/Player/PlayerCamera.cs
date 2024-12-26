using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.CanvasScaler;

public class PlayerCamera : MonoBehaviour
{
    public static readonly float BASE_CAMERA_SPEED_DEGREES = 1000f;
    public static readonly float CAM_ROTATION_RADIUS = 1f;

    private MechUnit mUnit;
    private float mHorizViewAngle = 0f;
    private float mVerticalViewAngle = 0f;

    [SerializeField] private GameObject mCameraObj;
    [SerializeField] private GameObject CloseReticle;
    [SerializeField] private GameObject FarReticle;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        mUnit = GetComponent<MechUnit>();
    }

    private void Update()
    {
        if (Mathf.Abs(Input.GetAxis("Mouse X")) > 0f || Mathf.Abs(Input.GetAxis("Mouse Y")) > 0f)
        {
            mHorizViewAngle += Input.GetAxis("Mouse X") * BASE_CAMERA_SPEED_DEGREES * Time.deltaTime;
            mVerticalViewAngle -= Input.GetAxis("Mouse Y") * BASE_CAMERA_SPEED_DEGREES * Time.deltaTime;

            Quaternion rot = Quaternion.Euler(mVerticalViewAngle, mHorizViewAngle, 0);

            mCameraObj.transform.rotation = rot;
            mCameraObj.transform.position = transform.position + mCameraObj.transform.forward * CAM_ROTATION_RADIUS;

            FarReticle.transform.rotation = rot;

            mUnit.RotateTowards(rot);
        }
    }

    public Quaternion GetViewDirection()
    {
        return Quaternion.LookRotation(mCameraObj.transform.forward, mCameraObj.transform.up);
    }
}
