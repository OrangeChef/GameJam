using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SavePlayer : MonoBehaviour
{

    #region Singleton
    public static SavePlayer Instance;

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

    public bool invertAim;
    public Transform startPosition;

    private Rigidbody2D body;

    void Start()
    {
        TryGetComponent(out body);
        StartCoroutine(Save(false));
    }

    public void SaveAimOnly(bool inverted)
    {
        invertAim = inverted;
        string[] newContents = File.ReadAllLines($"{Application.persistentDataPath}/BOING.txt");
        newContents[4] = $"{(inverted ? 1 : 0)}";
        File.WriteAllLines($"{Application.persistentDataPath}/BOING.txt", newContents);
    }

    public IEnumerator Save(bool useStartPos)
    {
        while (true)
        {
            if (!PlayerFaceCursor.Instance)
                yield break;

            invertAim = PlayerFaceCursor.Instance.invertAim;

            if (Directory.Exists($"{Application.persistentDataPath}/BOING.txt"))
                Directory.CreateDirectory($"{Application.persistentDataPath}/BOING.txt");

            string[] fileContents = new string[]
            {
            $"{(useStartPos ? startPosition.position.x : body.position.x)}",
            $"{(useStartPos ? startPosition.position.y : body.position.y)}",
            $"{(useStartPos ? 0f : body.velocity.x)}",
            $"{(useStartPos ? 0f : body.velocity.y)}",
            $"{Convert.ToInt32(invertAim)}"
            };

            File.WriteAllLines($"{Application.persistentDataPath}/BOING.txt", fileContents);

            yield return new WaitForSeconds(1f);
        }
    }
}
