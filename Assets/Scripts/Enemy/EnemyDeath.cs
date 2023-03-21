using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    [SerializeField] private ParticleSystem Death;
    [SerializeField] private Color SpiderColor;
    [SerializeField] private Color GoopColor;
    [SerializeField] private List<KeyController> keys;

    private int i = 0;

    private void Awake()
    {
        Death.gameObject.SetActive(false);
    }
    public void Death_(GameObject enemy_)
    {
        ParticleSystem.MainModule main= Death.main;
        Death.transform.position = enemy_.transform.position;
        if (enemy_.CompareTag("Spider"))
        {
            main.startColor = SpiderColor;
        }
        else if (enemy_.CompareTag("Goop"))
        {
            main.startColor = GoopColor;
        }
        else if (enemy_.CompareTag("Ghost"))
        {
            keys[i].gameObject.transform.position = enemy_.transform.position; ;
            keys[i].gameObject.SetActive(true);
            keys.RemoveAt(i);
        }
        Death.gameObject.SetActive(true);
        Destroy(enemy_);
    }
}
