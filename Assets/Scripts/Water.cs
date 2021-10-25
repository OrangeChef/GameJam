using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{

    public string sceneName;
    public float delay = 0.5f;

    [Space]
    public float upwardForce = 3f;

    private Rigidbody2D playerBody;

    void Update()
    {
        if (playerBody)
            playerBody.AddForce(Vector2.up * upwardForce, ForceMode2D.Force);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            playerBody = collision.GetComponent<Rigidbody2D>();
            StartCoroutine(ReloadScene());
        }
    }

    public IEnumerator ReloadScene()
    {
        yield return new WaitForSeconds(delay);

        GameManager.Instance.ClearPlayerPrefs();
        GameManager.Instance.LoadScene(sceneName);
    }
}
