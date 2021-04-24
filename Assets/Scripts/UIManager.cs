using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*UI管理*/
public class UIManager : MonoBehaviour
{
    public static UIManager instance { get; private set;}
    
    void Start() {
        instance = this;
    }
    public Image healthBar; //角色的血条
    //更新血条
    public void UpdateHealthBar(int curAmount, int maxAmount)
    {
        healthBar.fillAmount = (float)curAmount / (float)maxAmount;
    }
}
