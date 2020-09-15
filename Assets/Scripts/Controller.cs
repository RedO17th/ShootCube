using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private int countObjects;
    private GameObject objectsPrefab;
    private Boundary coordinates;
    private bool isNegative = false;
    private LvlController controller;


    public void Init(LvlController contr, int count, GameObject objects, Boundary coord, bool side = false)
    {
        controller = contr;
        countObjects = count;
        objectsPrefab = objects;
        coordinates = new Boundary();
        if (side)
        {
            isNegative = true;
            coordinates.xCoord = coord.xCoord * -1;
        }
        else
            coordinates.xCoord = coord.xCoord;
        coordinates.yMin = coord.yMin;
        coordinates.yMax = coord.yMax;

        InstObj();
    }
    //Инстанс объектов
    public void InstObj()
    {
        for (int i = 0; i < countObjects; i++)
        {
            InstOne();
        }
    }
    //По одному
    public void InstOne()
    {
        GameObject obj = Instantiate(objectsPrefab, GetPos(), Quaternion.identity);
        obj.GetComponent<FlyObj>().SetTag(transform.tag, this);
        obj.GetComponent<FlyObj>().SetForward(isNegative);
    }
    //Получение Рандомной позиции
    public Vector3 GetPos()
    {
        return new Vector3(coordinates.xCoord, Random.Range(coordinates.yMin, coordinates.yMax), transform.position.z);
    }
    //Проверка на столкновение и перемещение на другую позицию
    private void OnTriggerEnter(Collider coll)
    {
        if (coll.transform.tag != transform.tag && controller.GetScore() != 0)
        {
            coll.transform.GetComponent<FlyObj>().NewPos();
            controller.MinusScore();
        }
    }
    public void Play()
    {
        controller.PlaySound();
    }
    public void PlayP(Transform obj)
    {
        controller.PlayParticle(obj);
    }
}
