using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    [SerializeField] private GameObject coin;
    [SerializeField] private GameObject potion;
    [SerializeField] private ParticleSystem Death;
    [SerializeField] private Color SpiderColor;
    [SerializeField] private Color GoopColor;
    [SerializeField] private KeyController keys;

    private void Awake()
    {
        Death.gameObject.SetActive(false);
    }
    public void Death_(GameObject enemy_)
    {
        ParticleSystem.MainModule main= Death.main;
        Death.transform.position = enemy_.transform.position;
        SoundManager.Instance.Play(SoundEvents.EnemyDeath);
        if (enemy_.CompareTag("Spider"))
        {
            SpwanCollectibles(coin, enemy_.transform);   
            main.startColor = SpiderColor;
        }
        else if (enemy_.CompareTag("Goop"))
        {
            SpwanCollectibles(potion, enemy_.transform);
            main.startColor = GoopColor;
        }
        else if (enemy_.CompareTag("Ghost"))
        {
            keys.gameObject.transform.position = enemy_.transform.position; ;
            keys.gameObject.SetActive(true);
        }
        else if (enemy_.CompareTag("Boss"))
        {
            keys.gameObject.transform.position = enemy_.transform.position; ;
            keys.gameObject.SetActive(true);
        }
        Death.gameObject.SetActive(true);
        Destroy(enemy_);
    }

    private void SpwanCollectibles(GameObject objectToSpwan, Transform enemyTransform)
    {
        GameObject newCollectible = Instantiate(objectToSpwan, enemyTransform.position, enemyTransform.rotation, enemyTransform.parent);
    }
}
