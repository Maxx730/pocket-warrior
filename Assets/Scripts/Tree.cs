using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{

    [Header("General")]
    public float SwayAmount;

    private GameObject TreeTop, TreeMiddle;

    private void Start()
    {
        TreeTop = transform.GetChild(0).gameObject;
        TreeMiddle = transform.GetChild(1).gameObject;
    }

    void Update()
    {
        TreeTop.transform.localPosition = new Vector3(Mathf.Sin(Time.time) * SwayAmount, TreeTop.transform.localPosition.y, TreeTop.transform.localPosition.z);
        TreeMiddle.transform.localPosition = new Vector3(Mathf.Sin(Time.time) * (SwayAmount / 2), TreeMiddle.transform.localPosition.y, TreeMiddle.transform.localPosition.z);
    }
}
