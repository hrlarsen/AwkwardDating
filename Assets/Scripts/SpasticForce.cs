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
        _rb.AddForce(Force, ForceMode.Impulse);
    }


}
