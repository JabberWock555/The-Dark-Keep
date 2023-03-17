using System;
using System.Collections;
using UnityEngine;

public class Gates : MonoBehaviour
{
    [SerializeField]private PlayerController player;
    [SerializeField] private Animator animator;
    public GateDestination gate;

    private void Awake()
    {
        animator.enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player.gameObject)
        {
            StartCoroutine(Transport());
        }
    }

    private IEnumerator Transport()
    {
        animator.enabled = true;
        yield return new WaitForSeconds(0.3f);
        player.transform.position = gate.destination.position;
        animator.enabled = false;
    }
}

[Serializable]
public class GateDestination
{
    public GatesNo gateNo;
    public Transform destination;
}
public enum GatesNo
{
    Gate0,
    Gate1,
    Gate2,
    Gate3,
    Gate4,
    Gate5,
    Gate6,
    Gate7,
    Gate8,
    Gate9
}