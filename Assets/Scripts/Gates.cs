using System;
using System.Collections;
using UnityEngine;

public class Gates : MonoBehaviour
{
    [SerializeField]private PlayerController player;
    [SerializeField] private Keys keyManager;
    private Animator animator;

    public GateDestination gate;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        SetGateStatus(gate.gateNo, GateStatus.Locked);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player.gameObject )
        {
            switch (gate.gateType)
            {
                case GateType.KeyGate:
                    if (player.key == keyManager.GetKeyType(GetGateNo()))
                    {
                        SetGateStatus(gate.gateNo, GateStatus.Open);
                        StartCoroutine(Transport());
                    }
                    else
                    {
                        Debug.Log("Wrong Key");
                    }
                    break;

                case GateType.LeverGate:
                    if(GetGateStatus(gate.gateNo) == GateStatus.Open)
                    {
                        StartCoroutine(Transport());
                    }
                    else
                    {
                        Debug.Log("Open with Lever");
                    }
                    break;

                case GateType.none:
                    SetGateStatus(gate.gateNo, GateStatus.Open);
                    StartCoroutine(Transport());
                    break;
            }
        }
        else
        {
            Debug.Log("Locked");
        }
    }

    private IEnumerator Transport()
    {
        animator.SetBool("DoorOpen", true);
        yield return new WaitForSeconds(0.3f);
        player.transform.position = gate.destination.position;
        animator.SetBool("DoorOpen", false);
    }

    public GatesNo GetGateNo()
    {
        return gate.gateNo;
    }

    public GateStatus GetGateStatus(GatesNo _gateNo)
    {
        GateStatus gateStatus = (GateStatus)PlayerPrefs.GetInt(_gateNo.ToString(), 0);
        return gateStatus;
    }

    public void SetGateStatus(GatesNo _gateNo, GateStatus _gateStatus)
    {
        PlayerPrefs.SetInt(_gateNo.ToString(), (int)_gateStatus);
    }
}

[Serializable]
public class GateDestination
{
    public GatesNo gateNo;
    public GateType gateType;
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

public enum GateType
{
    KeyGate,
    LeverGate,
    none
}

public enum GateStatus
{
    Locked,
    Open
}