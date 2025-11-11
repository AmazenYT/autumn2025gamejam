using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaBar : MonoBehaviour
{

    [Range(0, 4000)]
    static public float stamina;
    public float maxStamina = 2000;

    public RectTransform uiBar;

    float barWidth;

    public bool staminaRegen = false;

    //public FirstPersonMovement script;


    private void Start()
    {
        barWidth = uiBar.anchorMax.x;
        stamina = maxStamina;

    }

    public void Update()
    {
        stamina = ((stamina > maxStamina) ? maxStamina : (stamina < 0) ? 0 : stamina);

        float x = ((stamina * (100f / maxStamina)) * (1f / barWidth)) / 100f;

        uiBar.anchorMax = new Vector2(x, uiBar.anchorMax.y);


        if (FirstPersonMovement.IsRunning == true)
        {
            stamina -= 15 * Time.deltaTime;
        }

        if (FirstPersonMovement.IsRunning == false)
        {
            stamina += 20 * Time.deltaTime;
        }

        if (stamina <= 0)
        {
            FirstPersonMovement.canRun = false;
        }

        if (stamina >= 25)
        {
            FirstPersonMovement.canRun = true;
        }

    }

}
