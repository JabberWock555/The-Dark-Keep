using System;
using System.Collections;
using UnityEngine;

public class Gates : MonoBehaviour 
{
    [SerializeField] private PlayerController player;
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
                        UiManager.keyUsed = true;
                        SetGateStatus(gate.gateNo, GateStatus.Open);
                        StartCoroutine(Transport());
                        ChangeRoom(GetGateNo());
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
                        ChangeRoom(GetGateNo());
                    }
                    else
                    {
                        Debug.Log("Open with Lever");
                    }
                    break;

                case GateType.none:
                    SetGateStatus(gate.gateNo, GateStatus.Open);
                    StartCoroutine(Transport());
                    ChangeRoom(GetGateNo());
                    break;
                default:
                    Debug.Log("Locked");
                    break;
            }
        }
    }

    private IEnumerator Transport()
    {
        animator.SetBool("DoorOpen", true);
        yield return new WaitForSeconds(0.2f);
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
    public void ChangeRoom(GatesNo exitGate)
    {
        switch (exitGate)
        {
            case GatesNo.Gate0:
                player.RoomEnter = RoomNo.Room1;
                player.RoomExit = RoomNo.Room0;
                break;
            case GatesNo.Gate1:
                player.RoomEnter = RoomNo.Room0;
                player.RoomExit = RoomNo.Room1;
                break;
            case GatesNo.Gate2:
                player.RoomEnter = RoomNo.Room2;
                player.RoomExit = RoomNo.Room1;
                break;
            case GatesNo.Gate3:
                player.RoomEnter = RoomNo.Room1;
                player.RoomExit = RoomNo.Room2;
                break;
            case GatesNo.Gate4:
                player.RoomEnter = RoomNo.Room3;
                player.RoomExit = RoomNo.Room1;
                break;
            case GatesNo.Gate5:
                player.RoomEnter = RoomNo.Room1;
                player.RoomExit = RoomNo.Room3;
                break;
            case GatesNo.Gate6:
                player.RoomEnter = RoomNo.Room4;
                player.RoomExit = RoomNo.Room3;
                break;
            case GatesNo.Gate7:
                player.RoomEnter = RoomNo.Room3;
                player.RoomExit = RoomNo.Room4;
                break;
            case GatesNo.Gate8:
                player.RoomEnter = RoomNo.Room5;
                player.RoomExit = RoomNo.Room1;
                break;
            case GatesNo.Gate9:
                player.RoomEnter = RoomNo.Room1;
                player.RoomExit = RoomNo.Room5;
                break;
        }
        player.roomChanged = true;
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
    Gate9,
    Prison
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