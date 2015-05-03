using UnityEngine;
using System.Collections;

public class PlayerDead : MonoBehaviour
{
    public GameObject Died;

    private Renderer render;

    private bool respawning;
    // Use this for initialization
    private void Start()
    {
        render = GetComponent<Renderer>();

        respawning = false;
    }

    // Update is called once per frame
    private void Update()
    {
        Debug.Log(render.isVisible);

        if (!render.isVisible && !respawning)
        {
            StartCoroutine(Respawn());
        }
    }

    IEnumerator Respawn()
    {
        respawning = true;
        Instantiate(Died, transform.position, Quaternion.identity);

        yield return new WaitForSeconds(2f);
        transform.position = ZoneManager.Instance.Spawner.position;

        respawning = false;
    }


}
