using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class okita_mana : MonoBehaviour
{
    public Image _Mana;

    public void capnhatthanhmana(float luongmanahientai, float luongmanatoida)
    {
        _Mana.fillAmount = luongmanahientai/luongmanatoida;
    }
}

