using UnityEngine;
using System.Collections;

public class AnimationEventHelper : MonoBehaviour {

    public void OnAnimationFinish()
    {
        gameObject.transform.parent.GetComponent<Player>().c_onAnimationEnded();
    }
}