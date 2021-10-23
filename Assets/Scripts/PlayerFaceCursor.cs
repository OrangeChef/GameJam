using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerFaceCursor : MonoBehaviour
{

    #region Singleton
    public static PlayerFaceCursor Instance;

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

    public bool invertAim = false;

    private Rigidbody2D body;
    private Vector2 directionToMouse;
    private Camera mainCam;

    void Start()
    {
        mainCam = Camera.main;
        TryGetComponent(out body);
        LoadGameData();
    }

    void Update()
    {
        body.rotation = GetAngle();
    }

    float GetAngle()
    {
        float angle;

        directionToMouse = transform.position - mainCam.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * 10f);
        angle = Mathf.Atan2(directionToMouse.y, directionToMouse.x) * Mathf.Rad2Deg + (invertAim ? -90f : 90f);
        body.rotation = angle * (invertAim ? -1f : 1f);

        return angle;
    }

    public void LoadGameData()
    {
        string[] saveData = File.ReadAllLines($"{Application.persistentDataPath}/BOING.txt");
        body.position = new Vector2(float.Parse(saveData[0]), float.Parse(saveData[1]));
        body.velocity = new Vector2(float.Parse(saveData[2]), float.Parse(saveData[3]));
        invertAim = saveData[4] == "1";
    }

    public void LoadAimOnly()
    {
        string[] saveData = File.ReadAllLines($"{Application.persistentDataPath}/BOING.txt");
        invertAim = saveData[4] == "1";
    }
}
