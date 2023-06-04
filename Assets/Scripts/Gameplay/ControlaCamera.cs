using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaCamera : MonoBehaviour
{
    public GameObject Jogador;
    Vector3 distCompensar;

    // Start is called before the first frame update
    public void Start()
    {
        distCompensar = transform.position - Jogador.transform.position;
    }

    // Update is called once per frame
    public void Update()
    {
        transform.position = Jogador.transform.position + distCompensar;
    }
}