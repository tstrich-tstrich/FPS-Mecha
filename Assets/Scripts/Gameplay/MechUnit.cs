using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class MechUnit : MonoBehaviour
{
    public static float VEL_NEAR_ZERO_CUTOFF = 0.1f;
    private const float BAD_FRAMES_COMPENSATE = 5f;
    [SerializeField] private float TEMP_MOVE_FORCE = 1f;
    [SerializeField] private float TEMP_TURN_SPEED_DEG = 90f;
    [SerializeField] private float TEMP_MASS = 1f;

    private Quaternion mTargetRotation;

    private Rigidbody mPhysics;
    private void Start()
    {
        mPhysics = GetComponent<Rigidbody>();
        mPhysics.mass = TEMP_MASS;
    }

    public void AccelerateTowards(Vector3 direction)
    {
        if(direction.sqrMagnitude != 1)
        {
            direction = direction.normalized;
        }

        mPhysics.AddForce(direction * TEMP_MOVE_FORCE, ForceMode.Impulse);
    }

    public void Brake()
    {
        if (mPhysics.linearVelocity.sqrMagnitude < VEL_NEAR_ZERO_CUTOFF)
        {
            return;
        }
        if (mPhysics.linearVelocity.magnitude * mPhysics.mass <= TEMP_MOVE_FORCE)
        {
            mPhysics.AddForce(-mPhysics.linearVelocity * mPhysics.mass, ForceMode.Impulse);
        }
        else
        {
            mPhysics.AddForce(-mPhysics.linearVelocity.normalized * TEMP_MOVE_FORCE, ForceMode.Impulse);
        }
    }

    public void RotateTowards(Quaternion newRotation)
    {
        mTargetRotation = newRotation;
        Debug.Log("New rotation target");
    }

    private void FixedUpdate()
    { 
        if (transform.rotation != mTargetRotation && (Quaternion.Angle(transform.rotation, mTargetRotation) <= (TEMP_TURN_SPEED_DEG * Time.deltaTime)))
        {           
            transform.rotation = mTargetRotation;
            //Debug.Log("Matching rotation to target");
        }
        else if(transform.rotation != mTargetRotation)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, mTargetRotation, TEMP_TURN_SPEED_DEG * Time.deltaTime);
            //Debug.Log("Rotating to target");
        }
        else
        {
            //Debug.Log("No Rotation logged");
        }
    }
}
