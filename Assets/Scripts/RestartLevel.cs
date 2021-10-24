using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartLevel : MonoBehaviour
{

    public string sceneName;
    public float delay = 0.5f;

    private Transform player;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.Equals(player))
            StartCoroutine(ReloadScene());
    }

    IEnumerator ReloadScene()
    {
        yield return new WaitForSeconds(delay);

        PlayerManager.Instance.SaveAll(true);
        GameManager.Instance.LoadScene(sceneName);
    }
}
