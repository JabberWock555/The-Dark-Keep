
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public GameObject[] Room;
    public PlayerController player;
    private void Awake()
    {
        foreach (GameObject room in Room)
        {
            room.SetActive(false);
        }
        Room[0].SetActive(true);
    }
    private void Update()
    {
        if (player.roomChanged)
        {
            Room[(int)player.RoomEnter].SetActive(true);
            Room[(int)player.RoomExit].SetActive(false);
            player.roomChanged = false;
        }
    }
}
public enum RoomNo
{
    Room0,
    Room1,
    Room2,
    Room3,
    Room4,
    Room5,
}