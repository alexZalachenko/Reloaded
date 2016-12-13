using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Clock : MonoBehaviour {

    [SerializeField]
    private Image c_numberDisplayed = null;
    [SerializeField]
    private Sprite[] c_numbers = null;
    [SerializeField]
    float c_countdownFrecuency = 1;
    private int c_currentNumber;

    public delegate void OnCountdownEnd();
    public OnCountdownEnd c_onCountDownEnd
    {
        set;
        get;
    }

    public IEnumerator StartCountDown()
    {
        c_currentNumber = c_numbers.Length;
        while (c_currentNumber>0)
        {
            
            c_numberDisplayed.sprite = c_numbers[c_currentNumber - 1];
            //playsound
            --c_currentNumber;
            yield return new WaitForSecondsRealtime(c_countdownFrecuency);
        }
        c_onCountDownEnd();
    }
}