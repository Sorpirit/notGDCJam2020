using System;
using UnityEngine;

public class GlobalTimer : MonoBehaviour {
    
    public static GlobalTimer timer;

    [SerializeField] private float StartTime;

    private float time;
    private bool timeOut;

    public float Countdown{get => time; set => time = value;}
    public event Action TimeIsOut;

    private void Awake()
    {
        if(timer == null){
            timer = this;
        }else if(timer != this){
            Destroy(gameObject);
            return;
        }
        time = StartTime;
        timeOut = false;
    }

    private void Update()
    {
        if(!timeOut) time -= Time.deltaTime;
        
        if(time <= 0 && !timeOut)
        {
            timeOut = true;
            TimeIsOut?.Invoke();
        }
    }

}