using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFinish : MonoBehaviour
{

    public float movePlayerSpeed = 0.2f;
    public LeanTweenType easeType;

    private Transform player;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.transform;
            player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            player.GetComponentInChildren<SpringController>().enabled = false;

            PlayerManager.Instance.SaveAll(true);
            PlayerManager.Instance.enabled = false;

            GameTimer.Instance.SetTimerActivity(false);

            if (!PlayerPrefs.HasKey("Record"))
                PlayerPrefs.SetFloat("Record", 999999f);

            if (GameTimer.Instance.currentTime < PlayerPrefs.GetFloat("Record"))
                PlayerPrefs.SetFloat("Record", GameTimer.Instance.currentTime);

            player.LeanMove(transform.position, movePlayerSpeed).setEase(easeType).setOnComplete(() => StartCoroutine(WaitAndLoad()));
        }
    }

    IEnumerator WaitAndLoad()
    {
        yield return new WaitForSeconds(1f);
        GameManager.Instance.LoadScene("FinishMenu");
    }
}
