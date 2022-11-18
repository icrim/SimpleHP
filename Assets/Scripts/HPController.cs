using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPController : MonoBehaviour
{
    private Image HPImage;
    private int HP;
    private const int HPMax = 30;
    // Start is called before the first frame update
    void Start()
    {
        HP = HPMax ;
        HPImage = GameObject.Find("HP").GetComponent<Image>();
        HPImage.fillAmount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            if (HP < HPMax)
            {
                ++HP;
                HPImage.fillAmount = (float)HP / HPMax;
            }
        }
        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            if (HP > 0)
            {
                --HP;
                HPImage.fillAmount = (float)HP / HPMax;
            }
        }
    }
}
