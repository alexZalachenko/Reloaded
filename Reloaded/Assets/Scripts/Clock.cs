using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour {

    #region VARIABLES
    [SerializeField]
    private Image c_numberDisplayed = null;
    [SerializeField]
    private Sprite[] c_numbers = null;
    [SerializeField]
    float c_countdownFrecuency = 1000;//milliseconds
    private int c_currentNumber;
    private float c_currentTime;
    private bool c_countdownEnabled = false;

    public delegate void OnCountdownEnd();
    public OnCountdownEnd c_onCountDownEnd
    {
        set;
        get;
    }
    #endregion

    private void Update()
    {
        if (c_countdownEnabled)
        {
            while (c_currentNumber > 0 && Time.time > c_currentTime)
            {
                c_numberDisplayed.sprite = c_numbers[c_currentNumber - 1];
                --c_currentNumber;
                c_currentTime = Time.time + c_countdownFrecuency;
            }
            if (c_currentNumber == 0)
            {
                c_countdownEnabled = false;
                c_onCountDownEnd();
            }
        }
    }

    public void StartCountDown()
    {
        c_currentTime = Time.time;
        c_countdownEnabled = true;
        c_currentNumber = c_numbers.Length;
    }

    //public IEnumerator StartCountDown()
    //{
    //    c_currentNumber = c_numbers.Length;
    //    while (c_currentNumber > 0)
    //    {
    //        c_currentNumber = c_numbers.Length;
    //        c_numberDisplayed.sprite = c_numbers[c_currentNumber - 1];
    //        --c_currentNumber;
    //        yield return new WaitForSecondsRealtime(c_countdownFrecuency);
    //    }
    //    c_onCountDownEnd();
    //}
}