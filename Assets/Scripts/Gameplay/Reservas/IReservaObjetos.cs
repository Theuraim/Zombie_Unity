using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IReservaObjetos
{
    void DevolverObjeto(GameObject objeto);
    GameObject PegarObjeto();
    bool TemObjeto();
}
