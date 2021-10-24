using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Smiley : MonoBehaviour
{
    void Update()
    {
        transform.up = Vector3.up;
    }

    public IEnumerator SetFace(string newFace, string nextFace, float time)
    {
        TMP_Text faceText = GetComponentInChildren<TMP_Text>();
        faceText.text = newFace;

        yield return new WaitForSeconds(time);

        faceText.text = nextFace;
    }
}
