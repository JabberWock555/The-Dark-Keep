using UnityEngine.Events;
using UnityEngine;

public class LeverController : MonoBehaviour
{
    [SerializeField]private Gates gateManager;
    [SerializeField]private Keys key;
    [SerializeField] private UnityEvent leverEvent;
    public LeverType leverType;
    private Animator animator;
    private bool inRange;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        animator.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inRange = true;
        }
    }

    private void Update()
    {
        if (inRange)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                leverEvent.Invoke();
            }
        }
    }
    public void PullLever()
    {
        animator.enabled = true;
        animator.Play("Lever");
        gateManager.SetGateStatus(key.GetGateNo(leverType), GateStatus.Open);
    }

}
