using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralAnimationHandler : MonoBehaviour
{
    [SerializeField] Animator anim;
    private void OnEnable()
    {
        FinishLineHandler.OnFinish += PlayAnimation;
    }
    private void OnDisable()
    {
        FinishLineHandler.OnFinish -= PlayAnimation;
    }
    public void PlayAnimation()
    {
        anim.enabled = true;
    }
}
