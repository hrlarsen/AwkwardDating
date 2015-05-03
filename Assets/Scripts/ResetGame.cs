using UnityEngine;
using System.Collections;

public class ResetGame : MonoBehaviour
{

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            Application.LoadLevel(0);
    }

}
