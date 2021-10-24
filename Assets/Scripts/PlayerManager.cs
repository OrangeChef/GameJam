using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerManager : MonoBehaviour
{

    #region Singleton
    public static PlayerManager Instance;

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
    public Transform startPosition;

    [Header("Saving")]
    public float saveInterval = 1f;

    private Rigidbody2D body;
    private Vector2 directionToMouse;
    private Camera mainCam;

    void Start()
    {
        mainCam = Camera.main;
        TryGetComponent(out body);
        GetInvertAim();

        if (PlayerPrefs.HasKey("XPos") && PlayerPrefs.HasKey("YPos"))
            LoadPosition();

        if (PlayerPrefs.HasKey("XVel") && PlayerPrefs.HasKey("YVel"))
            LoadVelocity();

        StartCoroutine(SavePlayer());
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

    public void SetInvertAim(Toggle toggle)
    {
        invertAim = toggle.isOn;
    }

    public void GetInvertAim()
    {
        if (PlayerPrefs.HasKey("InvertAim"))
            invertAim = PlayerPrefs.GetInt("InvertAim") == 1;
    }

    public bool ReturnInvertAim()
    {
        bool inverted = false;

        if (PlayerPrefs.HasKey("InvertAim"))
            invertAim = PlayerPrefs.GetInt("InvertAim") == 1;

        return inverted;
    }

    public void SavePosition(bool startPos)
    {
        PlayerPrefs.SetFloat("XPos", startPos ? startPosition.position.x : body.position.x);
        PlayerPrefs.SetFloat("YPos", startPos ? startPosition.position.y : body.position.y);
    }

    public void SaveVelocity(bool startPos)
    {
        PlayerPrefs.SetFloat("XVel", startPos ? 0f : body.velocity.x);
        PlayerPrefs.SetFloat("YVel", startPos ? 0f : body.velocity.y);
    }

    public void LoadPosition()
    {
        body.position = new Vector2(PlayerPrefs.GetFloat("XPos"), PlayerPrefs.GetFloat("YPos"));
    }

    public void LoadVelocity()
    {
        body.velocity = new Vector2(PlayerPrefs.GetFloat("XVel"), PlayerPrefs.GetFloat("YVel"));
    }

    public void SaveAll(bool startPos)
    {
        SavePosition(startPos);
        SaveVelocity(startPos);
    }

    public IEnumerator SavePlayer()
    {
        while(true)
        {
            SaveAll(false);
            yield return new WaitForSeconds(saveInterval);
        }
    }
}