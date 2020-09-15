using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyObj : MonoBehaviour
{
    //private string parentTag;
    private Controller parentC;
    private Rigidbody rb;
    private bool forward;
    //anim
    private float tumble = 5f;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Start()
    {
        StartCoroutine(TimeWait());
    }
    public void SetTag(string tag, Controller parent)
    {
        //parentTag = tag;
        transform.tag = tag;
        parentC = parent;
    }
    //При соударении с противоположным коллайдером вызвать получение новой позиции, если не был до этого удален.
    public void NewPos()
    {
        transform.position = parentC.GetPos();
    }
    public void SetForward(bool forwrd)
    {
        forward = forwrd;
    }
    //Ожидание времени и запуск
    IEnumerator TimeWait()
    {
        float time = Random.Range(2, 10);
        float speed = Random.Range(1, 3);
        yield return new WaitForSeconds(time);
        if (forward)
            rb.velocity = new Vector3(speed, 0f, 0f);
        else
            rb.velocity = new Vector3(-1 * speed, 0f, 0f);
        rb.angularVelocity = Random.insideUnitSphere * tumble;
    }
    //private void OnMouseDown()
    //{
    //    parentC.InstOne();
    //    parentC.Play();
    //    parentC.PlayP(transform);
    //    Destroy(gameObject);
    //}
    public void Shoot()
    {
        parentC.InstOne();
        parentC.Play();
        parentC.PlayP(transform);
        Destroy(gameObject);
    }
    //public string ReturnTag()
    //{
    //    return parentTag;
    //}
}
