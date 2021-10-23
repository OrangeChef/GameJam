using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerFaceCursor : MonoBehaviour
{

    public bool invertAim = false;

    private Rigidbody2D body;
    private Vector2 directionToMouse;
    private Camera mainCam;

    void Start()
    {
        mainCam = Camera.main;
        TryGetComponent(out body);
        GetAim();
    }

    void Update()
    {
        body.rotation = GetAngle();
    }

    float GetAngle()
    {
        float angle = 0f;

        directionToMouse = transform.position - mainCam.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * 10f);
        angle = Mathf.Atan2(directionToMouse.y, directionToMouse.x) * Mathf.Rad2Deg + (invertAim ? -90f : 90f);
        body.rotation = angle * (invertAim ? -1f : 1f);

        return angle;
    }

    public void GetAim()
    {
        invertAim = Convert.ToBoolean(PlayerPrefs.GetInt("InvertAim"));
    }
}
