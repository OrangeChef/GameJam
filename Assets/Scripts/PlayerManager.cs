using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

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

    [Header("Start")]
    public Transform startPosition;
    public Vector3 targetScale;
    public float scaleTime = 1f;
    public LeanTweenType scaleType;

    [Header("Face")]
    public float impactSpeedForHurtFace = 10f;

    [Header("Saving")]
    public float saveInterval = 1f;

    private Rigidbody2D body;
    private Vector2 directionToMouse;
    private Camera mainCam;

    void Start()
    {
        mainCam = Camera.main;
        TryGetComponent(out body);

        if (PlayerPrefs.HasKey("XPos") && PlayerPrefs.HasKey("YPos"))
            LoadPosition();
        else
        {
            SavePosition(true);
            LoadPosition();
        }

        if (PlayerPrefs.HasKey("XVel") && PlayerPrefs.HasKey("YVel"))
            LoadVelocity();
        else
        {
            SaveVelocity(true);
            LoadVelocity();
        }

        StartCoroutine(SavePlayer());
        Initialize();
    }

    void Initialize()
    {
        invertAim = PlayerPrefs.HasKey("Invert") ? PlayerPrefs.GetInt("Invert") == 1 : transform;

        transform.localScale = Vector3.zero;
        GameTimer.Instance.SetTimerActivity(false);
        body.constraints = RigidbodyConstraints2D.FreezeAll;
        transform.LeanScale(targetScale, scaleTime).setEase(scaleType).setOnComplete(ActivateStuff);
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

    public void ActivateStuff()
    {
        body.constraints = RigidbodyConstraints2D.None;
        GameTimer.Instance.SetTimerActivity(true);
    }

    public void SavePosition(bool startPos)
    {
        PlayerPrefs.SetFloat("XPos", startPos ? startPosition.position.x : body.position.x);
        PlayerPrefs.SetFloat("YPos", startPos ? startPosition.position.y : body.position.y + 0.25f);
    }

    public void LoadPosition()
    {
        body.position = new Vector2(PlayerPrefs.GetFloat("XPos"), PlayerPrefs.GetFloat("YPos"));
    }

    public void SaveVelocity(bool startPos)
    {
        PlayerPrefs.SetFloat("XVel", startPos ? 0f : body.velocity.x);
        PlayerPrefs.SetFloat("YVel", startPos ? 0f : body.velocity.y);
    }

    public void LoadVelocity()
    {
        body.velocity = new Vector2(PlayerPrefs.GetFloat("XVel"), PlayerPrefs.GetFloat("YVel"));
    }

    public void SaveTime(bool startPos)
    {
        if (startPos)
            GameTimer.Instance.currentTime = 0f;

        PlayerPrefs.SetFloat("Time", (float)GameTimer.Instance.currentTime);
    }

    public void LoadTime()
    {
        if (PlayerPrefs.HasKey("Time"))
            GameTimer.Instance.currentTime = PlayerPrefs.GetFloat("Time");
    }

    public void SaveInvert(Toggle t)
    {
        PlayerPrefs.SetInt("Invert", t.isOn ? 1 : 0);
        LoadInvert();
    }

    public void LoadInvert()
    {
        invertAim = PlayerPrefs.GetInt("Invert") == 1;
    }

    public void SaveAll(bool startPos)
    {
        SavePosition(startPos);
        SaveVelocity(startPos);
        SaveTime(startPos);
    }

    public void LoadAll()
    {
        LoadPosition();
        LoadVelocity();
        LoadTime();
    }

    public IEnumerator SavePlayer()
    {
        while (true)
        {
            SaveAll(false);
            yield return new WaitForSeconds(saveInterval);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (body.velocity.magnitude >= impactSpeedForHurtFace)
            StartCoroutine(GetComponentInChildren<Smiley>().SetFace("=(", "=)", 1.5f));
    }
}
