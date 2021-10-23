using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCheckpoint : MonoBehaviour
{

    public int thisCheckpoint;

    void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.Instance.SetCheckpoint(thisCheckpoint);
    }
}
