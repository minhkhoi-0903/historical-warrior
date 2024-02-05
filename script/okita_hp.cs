using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class okita_hp : MonoBehaviour
{
    public Image _hp;

    public void capnhatthanhmau(float luongmauhientai, float luongmautoida)
    {
        _hp.fillAmount = luongmauhientai/luongmautoida;
    }
}

