using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Boundary
{
    public float xCoord, yMin, yMax;
}
public class LvlController : MonoBehaviour
{
    [Range(3,5)]
    [SerializeField] private int countObjects;
    [SerializeField] private Boundary coord;
    [SerializeField] private GameObject objectsPrefab;
    private Transform leftSide;
    private int countL;
    private Transform rightSide;
    private int countR;
    private ParticleSystem particle;
    //Очки
    private int score = 10;
    //--------------------
    [SerializeField] private UIController uiControll;
    private bool thatsAll = false;
    //БонусныеОчки
    private const int shoots = 5;
    private int countShoots = 0;
    [SerializeField] private BonusBar bar;
    private bool closeShoot = true;
    [SerializeField] private float timeWaitShot;
    private const float delay = 0.1f;
    [SerializeField] private BController bonusController;
    //Время
    [SerializeField] private int time = 30;
    [SerializeField] private int bonusTime = 10;
    private bool isEndTime = false;
    void Awake()
    {
        leftSide = transform.GetChild(0);
        rightSide = transform.GetChild(1);
        particle = transform.GetChild(2).GetComponent<ParticleSystem>();
        Calculate();
    }
    void Start()
    {
        leftSide.GetComponent<Controller>().Init(this, countL, objectsPrefab, coord, true);
        rightSide.GetComponent<Controller>().Init(this, countR, objectsPrefab, coord);
        uiControll.SetData(time, this, score);
        bonusController.SetLvlC(this);
    }
    //Расчет количества объектов для каждой стороны
    private void Calculate()
    {
        if ((countObjects % 2) == 0)
        {
            countL = countObjects / 2;
            countR = countObjects / 2;
        }
        else
        {
            countObjects--;
            countL = (countObjects / 2) + 1;
            countR = (countObjects / 2);
        }
    }
    public void PlaySound()
    {
        GetComponent<AudioSource>().Play();
    }
    public void PlayParticle(Transform obj)
    {
        particle.transform.position = obj.position;
        particle.Play();
    }
    void Update()
    {
        if (!thatsAll)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray rayInput = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(rayInput, out RaycastHit hit))
                {
                    if (hit.transform.CompareTag("Left") || hit.transform.CompareTag("Right"))
                    {
                        score++;
                        SetScore();
                        SetShoots();
                        hit.transform.GetComponent<FlyObj>().Shoot();
                    }
                    else
                    {
                            //MinusScore();
                            countShoots = 0;
                            bar.SwitchOf();
                    }
                }
            }
            if (isEndTime)
            {
                StartEndGame();
            }
        }
    }
    private void StartEndGame()
    {
        thatsAll = true;
        uiControll.OpenEndGaameWindow();
        StartCoroutine(WaitReload());
    }
    IEnumerator WaitReload()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(0);
    }
    public void SetEndTimer()
    {
        isEndTime = true;
    }
    public void MinusScore()
    {
        score--;
        SetScore();
        if (score == 0)
        {
            StartEndGame();
        }
    }
    private void SetScore()
    {
        uiControll.SetScore(score);
    }
    //Устанавливаем количество бонусных
    private void SetShoots()
    {
        countShoots++;
        bar.AddShoot();
        if (countShoots == shoots)
        {
            //Действие по получению очков----------------------------------------------------------
            countShoots = 0;
            bar.GetTimeWait(timeWaitShot);
            bar.Switch();
            thatsAll = true;
            StartCoroutine(AfterBonus());
            bonusController.SetBonusObj();
        }
    }
    IEnumerator AfterBonus()
    {
        yield return new WaitForSeconds(timeWaitShot + delay);
        thatsAll = false;
    }
    public void SetBonusTime()
    {
        //Debug.Log("Пришло в Контроллер");
        uiControll.SetBonusTime(bonusTime);
    }
    public int GetScore()
    {
        return score;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
