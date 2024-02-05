using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class okita_armor : MonoBehaviour
{
    public Image _Armor;

    public void capnhatthanhGiap(float luongGiapHientai, float luongGiapToida)
    {
        _Armor.fillAmount = luongGiapHientai/luongGiapToida;
    }
}
