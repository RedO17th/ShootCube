using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BController : MonoBehaviour
{
    [SerializeField] private Boundary coord;
    [SerializeField] private GameObject prefabObj;
    [SerializeField] private float bonusLifeTime;
    private LvlController controller;
    public void SetLvlC(LvlController controller)
    {
        this.controller = controller;
    }
    public void SetBonusObj()
    {
        GameObject bonus = Instantiate(prefabObj, new Vector3(Random.Range(-coord.xCoord, coord.xCoord), Random.Range(coord.yMin, coord.yMax), transform.position.z), Quaternion.identity);
        bonus.GetComponent<Bonus>().SetLiveTime(bonusLifeTime);
        bonus.GetComponent<Bonus>().SetControll(this);
    }
    public void SetClick()
    {
        controller.SetBonusTime();
    }
}
