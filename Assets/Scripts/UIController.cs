using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private LvlController controller;
    [SerializeField] private GameObject endGameImg;
    [SerializeField] private TextMeshProUGUI timer;
    private int time = 0;
    [SerializeField] private TextMeshProUGUI score;
    private bool isEnd = false;
    //Открытие окна конца игры
    public void OpenEndGaameWindow()
    {
        endGameImg.SetActive(true);
        isEnd = true;
        UnActive();
    }
    public void SetData(int time, LvlController controller, int score)
    {
        this.time = time;
        this.controller = controller;
        SetTimer();
        SetScore(score);
        StartCoroutine(GameTime());
    }
    private void SetTimer()
    {
        if (!isEnd)
            timer.text = time.ToString();
        else
            timer.text = "";
    }
    public void SetScore(int score)
    {
        this.score.text = score.ToString();
    }
    //Счетчик времени и завершение игры
    IEnumerator GameTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            time--;
            SetTimer();
            if (time == 0)
                break;
        }
        controller.SetEndTimer();
    }
    //Выключение поля таймера и очков
    private void UnActive()
    {
        timer.text = "";
        score.text = "";
    }
    public void SetBonusTime(int bonus)
    {
        time += bonus;
    }

}
