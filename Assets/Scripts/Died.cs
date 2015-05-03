using UnityEngine;
using System.Collections;

public class Died : MonoBehaviour
{

    // Use this for initialization
    private void Start()
    {
        float x, y, z;

        x = Random.Range(1, 7);
        y = Random.Range(1, 5);
        z = Random.Range(1, 3);

        Vector3 f = new Vector3(x,y,z);
        iTween.PunchScale(gameObject, f, 0.5f);

        iTween.ShakePosition(ZoneManager.Instance.MainCamera, f, 0.12f);
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