using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Navball : MonoBehaviour
{
    public GameObject PlayerIndicator;
    public GameObject VelocityIndicator;
    public GameObject AccelIndicator;
    public TMP_Text InfoText;
    public TMP_Text BrakesOnOff;
    public Image BrakesBG;
    public Outline BrakesOutline;

    public float VelRadius = 2.5f;
    public float AccRadius = 2f;

    private GameObject player;
    private Rigidbody playerPhysics;
    private PlayerCamera playerCamera;
    private PlayerControl playerControl;

    private Quaternion cameraInverseLook;
    private bool brakesOnPrevious;

    void Start()
    {
        player = GameManager.instance.GetPlayer();
        playerCamera = player.GetComponent<PlayerCamera>();
        playerPhysics = player.GetComponent<Rigidbody>();
        playerControl = player.GetComponent<PlayerControl>();
        brakesOnPrevious = !playerControl.AutoStop;
    }

    void Update()
    {
        //navball
        cameraInverseLook = Quaternion.Inverse(playerCamera.GetViewDirection());
        PlayerIndicator.transform.rotation = cameraInverseLook * player.transform.rotation;
    
        if (playerPhysics.linearVelocity.magnitude > MechUnit.VEL_NEAR_ZERO_CUTOFF)
        {
            //float x = Mathf.Asin(playerPhysics.linearVelocity.y / playerPhysics.linearVelocity.z) * Mathf.Rad2Deg;
            //float y = Mathf.Asin(playerPhysics.linearVelocity.x / playerPhysics.linearVelocity.z) * Mathf.Rad2Deg;

            VelocityIndicator.transform.rotation = cameraInverseLook * Quaternion.LookRotation(playerPhysics.linearVelocity);
            VelocityIndicator.transform.localPosition = VelocityIndicator.transform.forward * VelRadius;
            VelocityIndicator.SetActive(true);
            InfoText.text = "relative speed [" + playerPhysics.linearVelocity.magnitude + "]\n";
        }
        else
        {
            VelocityIndicator.SetActive(false);
            InfoText.text = "relative speed [~0]\n";
        }

        if(playerControl.CurrentAccelDirection != Vector3.zero)
        {
            AccelIndicator.transform.rotation = cameraInverseLook * Quaternion.LookRotation(playerControl.CurrentAccelDirection);
            AccelIndicator.transform.localPosition = AccelIndicator.transform.forward * AccRadius;
            AccelIndicator.SetActive(true);
        }
        else
        {
            AccelIndicator.SetActive(false);
        }

        //brakes indicator
        if(playerControl.AutoStop && !brakesOnPrevious)
        {
            //turn on brake indicator
            BrakesBG.enabled = true;
            BrakesOutline.enabled = false;
            BrakesOnOff.text = "ON";
            BrakesOnOff.color = new Color(0.02149509f, 0, 0.1257861f);
            brakesOnPrevious = playerControl.AutoStop;
        }
        else if(!playerControl.AutoStop && brakesOnPrevious)
        {
            //turn off brake indicator
            BrakesBG.enabled = false;
            BrakesOutline.enabled = true;
            BrakesOnOff.text = "OFF";
            BrakesOnOff.color = new Color(1, 0.1256574f, 0);
            brakesOnPrevious = playerControl.AutoStop;
        }
    }
}
