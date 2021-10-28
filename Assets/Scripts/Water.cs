using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{

    public string sceneName;
    public float delay = 0.5f;

    [Space]
    public AudioClip splash;
    public ParticleSystem splashParticles;

    private AudioSource source => GetComponent<AudioSource>();

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            source.PlayOneShot(splash);
            splashParticles.Play();

            StartCoroutine(Smiley.Instance.SetFace("=(", "=(", 5f));
            StartCoroutine(ReloadScene());
        }
    }

    public IEnumerator ReloadScene()
    {
        yield return new WaitForSeconds(delay);

        GameManager.Instance.ClearPlayerPrefs(true);
        GameManager.Instance.LoadScene(sceneName);
    }
}
