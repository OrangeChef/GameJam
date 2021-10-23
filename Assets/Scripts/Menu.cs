using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{

    public string menuName;
    public bool isMenuOpen;

    void Start()
    {
        gameObject.SetActive(isMenuOpen);
    }

    public void Open()
    {
        isMenuOpen = true;
        gameObject.SetActive(true);
    }

    public void Close()
    {
        isMenuOpen = false;
        gameObject.SetActive(false);
    }
}
