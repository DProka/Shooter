using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] GameObject barMain;
    [SerializeField] Image healthImage;

    public void Init()
    {
        barMain.SetActive(false);
        healthImage.transform.localScale = new Vector3(1f, 1f);
    }

    public void UpdateBar()
    {
        transform.rotation = Camera.main.transform.rotation;
    }

    public void SetHealth(float damage)
    {
        barMain.SetActive(true);
        float newHP = healthImage.transform.localScale.x - (damage * 0.01f);
        healthImage.transform.localScale = new Vector3 (newHP, 1f);

        if(healthImage.transform.localScale.x <= 0)
        {
            healthImage.transform.localScale = new Vector3(0f, 1f);
        }
    }
}
