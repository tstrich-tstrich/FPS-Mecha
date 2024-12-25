using TMPro;
using UnityEngine;

public class Navball : MonoBehaviour
{
    public GameObject PlayerIndicator;
    public GameObject VelocityIndicator;
    public GameObject CameraIndicator;
    public TMP_Text InfoText;

    public float VelRadius = 2.5f;
    public float CamRadius = 5f;

    private GameObject player;
    private Rigidbody playerPhysics;
    private PlayerCamera playerCamera;

    void Start()
    {
        player = GameManager.instance.GetPlayer();
        playerCamera = player.GetComponent<PlayerCamera>();
        playerPhysics = player.GetComponent<Rigidbody>();
    }

    void Update()
    {
        PlayerIndicator.transform.rotation = player.transform.rotation;

        CameraIndicator.transform.rotation = playerCamera.GetViewDirection();
        CameraIndicator.transform.localPosition = CameraIndicator.transform.forward * CamRadius;

        if (playerPhysics.linearVelocity.magnitude > 7)
        {
            //float x = Mathf.Asin(playerPhysics.linearVelocity.y / playerPhysics.linearVelocity.z) * Mathf.Rad2Deg;
            //float y = Mathf.Asin(playerPhysics.linearVelocity.x / playerPhysics.linearVelocity.z) * Mathf.Rad2Deg;

            VelocityIndicator.transform.rotation = Quaternion.LookRotation(playerPhysics.linearVelocity);
            VelocityIndicator.transform.localPosition = VelocityIndicator.transform.forward * VelRadius;
            VelocityIndicator.SetActive(true);
            InfoText.text = $"relative speed [" + playerPhysics.linearVelocity.magnitude + "]\n";
        }
        else
        {
            VelocityIndicator.SetActive(false);
            InfoText.text = $"relative speed [~0]\n";
        }

        
    }
}
