using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class AutoScale : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDisable()
    {
        this.transform.DOScale(Vector3.zero, 0f);
    }

    private void OnEnable()
    {
        this.transform.DOScale(Vector3.one, 0.35f).SetDelay(0.2f);
    }
}
