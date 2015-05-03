using UnityEngine;
using System.Collections;

public class SpasticForce : MonoBehaviour
{
    public Vector3 Force;
    public float ForceStrength = 10f;
    private Rigidbody _rb;
    // Use this for initialization
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }


    public void AddImpulseForce()
    {
        float x, y, z;

        x = Random.Range(20, 48);
        y= Random.Range(20, 43);
        z = Random.Range(10, 30);

        Vector3 force = new Vector3(x,y,z);
        _rb.AddForce(force, ForceMode.Impulse);
    }


}
