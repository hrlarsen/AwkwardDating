using UnityEngine;
using System.Collections;
public class Controls : MonoBehaviour {
    public KeyCode left, right, up, down, impulse;
    public float speed = 10f;
    public Rigidbody Myrigidbody;

   public ZoneTrigger myLastZone;

    // Use this for initialization
    void OnTriggerEnter(Collider trigger)
    {
        Debug.Log("The trigger has been triggered:" + trigger.name);

        trigger.GetComponent<ZoneTrigger>().counter++;

        myLastZone = trigger.GetComponent<ZoneTrigger>();

    }
    void OnTriggerExit(Collider trigger)
    {
        trigger.GetComponent<ZoneTrigger>().counter--;
        Debug.Log("The trigger has been NOT triggered:" + trigger.name);

        //myLastZone = null;
    }
    void Start () {
        Myrigidbody = gameObject.GetComponent<Rigidbody>();
        Myrigidbody.useGravity = false;
	}

    // Update is called once per frame
    void FixedUpdate()
    {

        if (Input.GetKeyDown(impulse))
            if (myLastZone != null)
                myLastZone.impulseCounter++;

            if (Input.GetKey(up))
            Myrigidbody.AddForce(Vector3.up * speed);
        if (Input.GetKey(down))
            Myrigidbody.AddForce(Vector3.up * -speed);
        if (Input.GetKey(left))
            Myrigidbody.AddForce(Vector3.right * -speed);
        if (Input.GetKey(right))
            Myrigidbody.AddForce(Vector3.right * speed);

    }
}

