using UnityEngine;
using System.Collections;

public class PlayerDead : MonoBehaviour
{
    public GameObject Died;

    private Renderer render;

    private bool respawning;
    private bool died;

    private TextMesh text;

    // Use this for initialization
    private void Start()
    {
        text = GetComponent<TextMesh>();


        render = GetComponent<Renderer>();

        respawning = false;

        died = false;
    }

    // Update is called once per frame
    private void Update()
    {
        Debug.Log(render.isVisible);

        if (!render.isVisible && !respawning & !died)
        {
            StartCoroutine(Respawn());
        }
    }

    IEnumerator Respawn()
    {
        respawning = true;

        float x, y, z;
        x = Random.Range(-20, 20);
        y = Random.Range(-10, 10);
        z = 0;

        Vector3 offset = new Vector3(x, y, z);
        GameObject g = (GameObject)Instantiate(Died, ZoneManager.Instance.Spawner.position + offset, Quaternion.identity);
        g.GetComponent<TextMesh>().color = GetComponent<UserMovement>().myColor;

        yield return new WaitForSeconds(2f);
        transform.position = new Vector3(-1000, -1000, -1000);

        respawning = false;

        died = true;
    }


}
