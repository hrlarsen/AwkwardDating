using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CameraMover : MonoBehaviour
{

    private Transform _transform;
    public float Speed = 10f;

    private bool Ready = false;

    public GameObject StartButton;

    // Use this for initialization
    private void Start()
    {
        _transform = transform;
    }

    public List<GameObject> Borders;

    public void StartGame()
    {
        Ready = true;

        StartButton.SetActive(false);

        GetComponent<AudioSource>().Play();

        foreach(GameObject g in Borders)
            g.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();


        if (Input.GetKeyDown(KeyCode.G))
            Ready = true;

        if (!Ready)
            return;

        transform.Translate(Vector3.right * ZoneManager.Instance.CameraMoveSpeed * Time.deltaTime);
    }
}
