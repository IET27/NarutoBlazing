using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HealthBar : MonoBehaviour{
    
    private Slider healthBar;
    [SerializeField]
    private TextMeshProUGUI maxHealthTXT, currentHealthTXT;

    private void Start() {
        healthBar = GetComponent<Slider>();    
    }

    public void setMaxHealth(int value){
        healthBar.maxValue = value;
        maxHealthTXT.SetText(value.ToString());
        currentHealthTXT.SetText(value.ToString());
        healthBar.value = value;
    }

    public void setHealth(int value){
        currentHealthTXT.SetText(value.ToString());
        healthBar.value = value;
    }
    
}
