using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class TimeVisualiser : MonoBehaviour {
    
    private TMP_Text timerUI;

    private void Awake()
    {
        timerUI = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        timerUI.text = (int)GlobalTimer.timer.Countdown/4 + " ";
        //timerUI.text = (int) GlobalTimer.timer.Countdown + ":" +(int) (100*(GlobalTimer.timer.Countdown - (int) GlobalTimer.timer.Countdown));
    }

}