
using UnityEngine;
using UnityEngine.UI;

public class KeyController : MonoBehaviour
{
    public KeyType keyType;
    [SerializeField] private UiManager UI;
    private Sprite img;

    private void Awake()
    {
        img = GetComponent<SpriteRenderer>().sprite;
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<PlayerController>() != null)
        {
            UI.KeyCollected(img);
            Destroy(gameObject);
        }
        
    }
}