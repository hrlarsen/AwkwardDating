using UnityEngine;
using System.Collections;

public class RandomShake : MonoBehaviour
{

    public Vector3 RBVelocity;


    public float MinX, MaxX;
    public float MinY, MaxY;
    public float MinZ, MaxZ;

    private Transform _transform;
    private Vector3 _startPos;
    private Rigidbody _rb;

    // Use this for initialization
    private void Start()
    {
        _transform = transform;
        _startPos = _transform.position;
        _rb = GetComponent<Rigidbody>();

        //InvokeRepeating("Shake", 1, 0.1f);
    }

    void FixedUpdate()
    {
        RBVelocity = _rb.velocity;

        Vector3 force = Vector3.zero;

        // left
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            force.x += Random.Range(-MinX, -MaxX);

        // right
        if (Input.GetKeyDown(KeyCode.RightArrow))
            force.x += Random.Range(MinX, MaxX);

        // up
        if (Input.GetKeyDown(KeyCode.UpArrow))
            force.y += Random.Range(MinY, MaxY);

        // down
        if (Input.GetKeyDown(KeyCode.DownArrow))
            force.y += Random.Range(-MinY, -MaxY);

        // in
        if (Input.GetKeyDown(KeyCode.K))
            force.z += Random.Range(-MinZ, -MaxZ);

        // out
        if (Input.GetKeyDown(KeyCode.L))
            force.z += Random.Range(MinZ, MaxZ);

         _rb.AddForce(force);

        if (Input.GetKeyDown(KeyCode.R))
        {
            _rb.velocity = Vector3.zero;
            _rb.Sleep();
        }
    }

    // Update is called once per frame
    private void Shake()
    {
        //_transform.position = _startPos + new Vector3(Random.Range(Min, Max), Random.Range(Min, Max), Random.Range(Min, Max));
    }
}
