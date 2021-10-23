using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [Space]
    public Menu[] allMenus;

    void Start()
    {
        if (allMenus.Length == 0)
            allMenus = FindObjectsOfType<Menu>();
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
}
