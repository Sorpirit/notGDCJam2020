using System;
using UnityEngine;

public class GlobalTimer : MonoBehaviour {
    
    public static GlobalTimer timer;

    [SerializeField] private float StartTime;

    private float time;
    private bool timeOut;
    private bool isTimerRunning;

    public float Countdown{get => time; set => time = value;}
    public bool IsTimerRunning
    {
        get => isTimerRunning; 
        set
        {
            TimerStateChenged?.Invoke();
            isTimerRunning = value;
        }
    }
    public event Action TimeIsOut;
    public event Action TimerStateChenged;

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
        if(isTimerRunning && !timeOut) time -= Time.deltaTime;
        
        if(time <= .5f)
        {
            timeOut = true;
            TimeIsOut?.Invoke();
        }
    }
    

}