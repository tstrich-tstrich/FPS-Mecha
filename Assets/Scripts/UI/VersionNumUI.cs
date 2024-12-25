using UnityEngine;
using TMPro;

public class VersionNumUI : MonoBehaviour
{
    void Start()
    {
        GetComponent<TMP_Text>().text = "//armorcontrol//v" + Application.version.ToString() + Random.value.ToString();
    }
}
