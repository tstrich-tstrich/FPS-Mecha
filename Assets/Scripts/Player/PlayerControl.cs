using UnityEngine;
[RequireComponent(typeof(PlayerCamera))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MechUnit))]

public class PlayerControl : MonoBehaviour
{
    public static KeyCode FORWARD = KeyCode.W;
    public static KeyCode BACK = KeyCode.S;
    public static KeyCode LEFT = KeyCode.A;
    public static KeyCode RIGHT = KeyCode.D;
    public static KeyCode UP = KeyCode.Space;
    public static KeyCode DOWN = KeyCode.LeftControl;
    public static KeyCode STOP = KeyCode.X;
    public bool AutoStop = true;

    //public static float MOVE_FORCE = 2f;
    //public static float JUMP_FORCE = 20f;

    private Rigidbody mPhysics;
    private PlayerCamera mCamera;
    private MechUnit mUnit;

    private Vector3 mDirection = Vector3.zero;

    private void Awake()
    {
        mPhysics = GetComponent<Rigidbody>();
        mCamera = GetComponent<PlayerCamera>();
        mUnit = GetComponent<MechUnit>();
    }


    private void Update()
    {
        mUnit.RotateTowards(mCamera.GetViewDirection());

        mDirection = Vector3.zero;
        if (Input.GetKey(FORWARD))
        {
            mDirection += transform.forward;
        }

        if (Input.GetKey(BACK))
        {
            mDirection -= transform.forward;
        }

        if (Input.GetKey(RIGHT))
        {
            mDirection += transform.right;
        }

        if (Input.GetKey(LEFT))
        {
            mDirection -= transform.right;
        }

        if (Input.GetKey(UP))
        {
            mDirection += transform.up;
        }

        if (Input.GetKey(DOWN))
        {
            mDirection -= transform.up;
        }

        if(mDirection != Vector3.zero)
        {
            mUnit.AcclerateTowards(mDirection);
        }
        else if (AutoStop || Input.GetKey(STOP))
        {
            mUnit.Brake();
        }
    }
}