using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusBar : MonoBehaviour
{
    private Image[] arrayShoots;
    private int count = 0;
    private float wait = 0;
    void Awake()
    {
        arrayShoots = new Image[transform.childCount];
        InitArray();
        SwitchOf();
    }
    //Выключаем полосы с выстрелами
    public void SwitchOf()
    {
        foreach (Image item in arrayShoots)
        {
            item.enabled = false;
        }
        count = 0;
    }
    private void InitArray()
    {
        for (int i = 0; i < arrayShoots.Length; i++)
        {
            arrayShoots[i] = transform.GetChild(i).GetComponent<Image>();
        }
    }
    public void AddShoot()
    {
        arrayShoots[count].enabled = true;
        count++;
        if (count > arrayShoots.Length)
        {
            count = 0;
            Switch();
        }
    }
    public void Switch()
    {
        StartCoroutine(Off());
    }
    IEnumerator Off()
    {
        yield return new WaitForSeconds(wait);
        SwitchOf();
    }
    public void GetTimeWait(float time)
    {
        wait = time;
    }
}
