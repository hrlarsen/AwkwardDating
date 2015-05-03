using UnityEngine;
using System.Collections;

public class CameraSpeedSetter : MonoBehaviour
{
    public float CameraSpeed = 2f;
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "MainCamera")
        {
            ZoneManager.Instance.CameraMoveSpeed = CameraSpeed;

            Debug.Log("new speed");
        }
    }
}
