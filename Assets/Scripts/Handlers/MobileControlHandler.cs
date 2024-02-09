using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileControlHandler : MonoBehaviour
{
    public GameObject mobileControls;

    public void EnableMobileControls()
    {
        mobileControls.SetActive(true);
    }

    public void DisableMobileControls()
    {
        mobileControls.SetActive(false);
    }
}
