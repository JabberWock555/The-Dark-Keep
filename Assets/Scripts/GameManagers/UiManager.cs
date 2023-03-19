using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class UiManager : MonoBehaviour
{
    public static bool keyUsed;

    [SerializeField] private PlayerController player;
    [SerializeField] private Slider healthBar;
    [SerializeField] private Image keyHolder;

    private void Update()
    {
        healthBar.value = player.Health;

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
