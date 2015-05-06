using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Died : MonoBehaviour
{

    private UserMovement useMovement;

    // Use this for initialization
    private void Start()
    {

        useMovement = GetComponent<UserMovement>();

        //text.color = useMovement.myColor;


        float x, y, z;

        x = Random.Range(0.2f, 2f);
        y = Random.Range(0.3f, 3f);
        z = 0;

        Vector3 f = new Vector3(x,y,z);
        iTween.PunchScale(gameObject, f, 0.5f);

        Transform camDefault = ZoneManager.Instance.MainCamera.transform;
        //iTween.ShakePosition(ZoneManager.Instance.MainCamera, f, 0.12f);
    }

    // Update is called once per frame

    private float time;

    private void Update()
    {
        time += Time.deltaTime;

        if (time > 2)
            Destroy(gameObject);
    }
}