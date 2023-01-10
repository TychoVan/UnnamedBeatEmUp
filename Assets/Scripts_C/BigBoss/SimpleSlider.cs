using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleSlider : MonoBehaviour
{
   [SerializeField]private Image slider;

    public TemporaryEnemyHP BossHp;

    public int CurrentHP;
    public int targetHP;

    private void Start()
    {
        slider = FindObjectOfType<Image>();        
    }

    public void ChangeSlider()
    {
         
    }
}
