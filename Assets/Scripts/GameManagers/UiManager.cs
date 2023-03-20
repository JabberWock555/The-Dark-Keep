using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class UiManager : MonoBehaviour
{
    public static bool keyUsed;
    public static bool showText;
    public static string msg;

    [SerializeField] private PlayerController player;
    [SerializeField] private BossMovement boss;
    [SerializeField] private Slider playerHealthBar;
    [SerializeField] private Slider bossHealthBar;
    [SerializeField] private GameObject bottomPanel;
    [SerializeField] private Text msgBox;
    [SerializeField] private Image keyHolder;

    private void Awake()
    {
        bottomPanel.SetActive(false);
    }

    private void Update()
    {
        playerHealthBar.value = player.Health;

        if (player.RoomEnter == RoomNo.Room5)
        {
            bossHealthBar.gameObject.SetActive(true);
            bossHealthBar.value = boss.Health;
        }
        if (keyUsed)
        {
            keyHolder.enabled = false;
            keyUsed = false;
        }

        if (showText)
        {
            msgBox.text= msg;
            StartCoroutine(Display());
        }

    }

    public void KeyCollected(Sprite img)
    {
        keyHolder.enabled = true;
        keyHolder.sprite = img;
    }

    private IEnumerator Display()
    {
        bottomPanel.SetActive(true);
        yield return new WaitForSeconds(3f);
        bottomPanel.SetActive(false);
        showText = false;
    }

    public static void ShowMsg(string text)
    {
        showText = true;
        msg = text;
    }
   
}
