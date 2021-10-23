using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

    #region Singleton
    public static MenuManager Instance;

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

    public float resetBounceDelay = 0.1f;
    public Toggle invertAimToggle;

    [Space]
    public Menu[] allMenus;

    void Start()
    {
        if (allMenus.Length == 0)
            allMenus = FindObjectsOfType<Menu>();

        if (PlayerPrefs.HasKey("InvertAim") && invertAimToggle)
            invertAimToggle.isOn = Convert.ToBoolean(PlayerPrefs.GetInt("InvertAim"));
    }

    public void OpenMenu(Menu menu)
    {
        foreach(Menu m in allMenus)
        {
            CloseMenu(m);
        }

        if (!menu.isMenuOpen)
            menu.Open();
    }

    public void CloseMenu(Menu menu)
    {
        if (menu.menuName == "Pause")
            StartCoroutine(FindObjectOfType<SpringController>().ResetBounceAbility(resetBounceDelay));

        if (menu.isMenuOpen)
            menu.Close();
    }

    public void SetPlayerAim()
    {
        if (invertAimToggle)
            PlayerPrefs.SetInt("InvertAim", Convert.ToInt32(invertAimToggle.isOn));
    }
}
