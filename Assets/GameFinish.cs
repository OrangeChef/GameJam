using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFinish : MonoBehaviour
{

    public float movePlayerSpeed = 0.2f;
    private Transform player;
    private Vector2 refVector;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.transform;

            foreach(MonoBehaviour comp in collision.GetComponents<MonoBehaviour>())
            {
                comp.enabled = false;
                StartCoroutine(MovePlayerToCenter());
            }
        }
    }

    IEnumerator MovePlayerToCenter()
    {
        player.position = transform.position;
        yield return new WaitForSeconds(2f);
        GameManager.Instance.LoadScene("Finish");
    }
}
