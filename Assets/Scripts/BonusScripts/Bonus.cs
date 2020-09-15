using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    private float lifeTime;
    private BController controller;
    private void Start()
    {
        StartCoroutine(LiveTime());
    }
    public void OnClick()
    {
        controller.SetClick();
        Enable();
    }
    IEnumerator LiveTime()
    {
        yield return new WaitForSeconds(lifeTime);
        Enable();
    }
    private void Enable()
    {
        Destroy(gameObject);
    }
    public void SetLiveTime(float time)
    {
        lifeTime = time;
    }
    public void SetControll(BController controller)
    {
        this.controller = controller;
    }
}
