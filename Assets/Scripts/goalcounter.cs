using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class goalcounter : MonoBehaviour
{
    public int piececount = 0;
    public TextMeshProUGUI text;
    public void updateCount()
    {
        text.text = piececount.ToString() + "/50";
    }

}
