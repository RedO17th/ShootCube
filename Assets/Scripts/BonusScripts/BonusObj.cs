using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusObj : MonoBehaviour
{
    private Bonus parent;
    void Awake()
    {
        parent = transform.parent.GetComponent<Bonus>();
    }
    private void OnMouseDown()
    {
        parent.OnClick();
    }
}
