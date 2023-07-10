using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] GameObject healthBar;
    [SerializeField] private Image playerHealthImage;
    [SerializeField] private TextMeshProUGUI healthText;

    private float maxHealth=100;
    private int health=100;

    public GameObject healthCanvas;
    

  
    public void HealthUpdate(float health)
    {
        if (!healthBar.activeSelf)
        {
            healthBar.SetActive(true);
        }
        
        playerHealthImage.fillAmount = (health / maxHealth);
        healthText.text = health.ToString();
       
    }
    private void LateUpdate()
    {
        healthCanvas.transform.LookAt(Camera.main.transform);
    }
}
