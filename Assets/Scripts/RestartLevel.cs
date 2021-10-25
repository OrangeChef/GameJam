using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartLevel : MonoBehaviour
{

    public string sceneName;
    public float delay = 0.5f;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
            StartCoroutine(ReloadScene());
    }

    public IEnumerator ReloadScene()
    {
        yield return new WaitForSeconds(delay);

        GameManager.Instance.ClearPlayerPrefs();
        GameManager.Instance.LoadScene(sceneName);
    }
}
