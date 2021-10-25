using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Smiley : MonoBehaviour
{

    #region Singleton
    public static Smiley Instance;

    void Awake()
    {
        if (Instance && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
    }
    #endregion

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
