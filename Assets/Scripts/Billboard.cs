using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private void Update()
    {
        this.transform.LookAt(this.transform.position + Camera.main.transform.forward);
    }
}
