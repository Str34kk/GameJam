using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public GameObject win;

    [System.Obsolete]
    public void OnBossKill()
    {
        win.active = true;
    }
}
