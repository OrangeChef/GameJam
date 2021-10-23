using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerFaceCursor : MonoBehaviour
{

    private Rigidbody2D body;
    private Vector2 directionToMouse;
    private Camera mainCam;

    void Start()
    {
        mainCam = Camera.main;
        TryGetComponent(out body);
    }

    void Update()
    {
        directionToMouse = transform.position - mainCam.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * 10f);
        float angle = Mathf.Atan2(directionToMouse.y, directionToMouse.x) * Mathf.Rad2Deg + 90f;
        body.rotation = angle;
    }
}
