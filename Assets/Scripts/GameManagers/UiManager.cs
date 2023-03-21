using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class UiManager : MonoBehaviour
{
    public static bool keyUsed;

    [SerializeField] private PlayerController player;
    [SerializeField] private BossMovement boss;
    [SerializeField] private Slider playerHealthBar;
    [SerializeField] private Slider bossHealthBar;
    [SerializeField] private Image keyHolder;

    private void Update()
    {
        playerHealthBar.value = player.Health;

        if(player.RoomEnter == RoomNo.Room5)
        {
            bossHealthBar.gameObject.SetActive(true);
            bossHealthBar.value = boss.Health;
        }
        if (keyUsed)
        {
            keyHolder.enabled = false;
            keyUsed = false;
        }

    }

    public void KeyCollected(Sprite img)
    {
        keyHolder.enabled = true;
        keyHolder.sprite = img;
    }
}
