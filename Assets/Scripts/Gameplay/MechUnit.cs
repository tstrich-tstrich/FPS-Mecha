using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class MechUnit : MonoBehaviour
{
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

    public void AcclerateTowards(Vector3 direction)
    {
        if(direction.sqrMagnitude != 1)
        {
            direction = direction.normalized;
        }

        mPhysics.AddForce(direction * TEMP_MOVE_FORCE, ForceMode.Impulse);
    }

    public void Brake()
    {
        if (mPhysics.linearVelocity.sqrMagnitude == 0)
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
    }

    private void FixedUpdate()
    {
        if(transform.rotation != mTargetRotation && (Quaternion.Angle(transform.rotation, mTargetRotation) < (TEMP_TURN_SPEED_DEG * Time.deltaTime)))
        {
            transform.rotation = mTargetRotation;
        }
        else
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, mTargetRotation, TEMP_TURN_SPEED_DEG * Time.deltaTime);
        }
    }
}
