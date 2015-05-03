using UnityEngine;
using System.Collections;

public class CameraMover : MonoBehaviour
{

    private Transform _transform;
    public float Speed = 10f;

    private bool Ready = false;

    // Use this for initialization
    private void Start()
    {
        _transform = transform;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
            Ready = true;

        if (!Ready)
            return;

        transform.Translate(Vector3.right * Speed * Time.deltaTime);
    }
}
