using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandeling : MonoBehaviour
{
    public void DisableHandling()
    {
        ButtonZone[] buttons = FindObjectsOfType<ButtonZone>();
        if (buttons != null)
        {
            float difference = 0;
            foreach (ButtonZone button in buttons)
            {
                difference = Vector3.Distance(transform.position, button.transform.position);
                if (difference < 1f)
                {
                    button.itemsInZone--;
                }
            }
        }
    }
    private void OnEnable()
    {
        transform.parent = null;

        //EnableHandling();
    }
    private void OnDestroy()
    {
        ButtonZone[] buttons = FindObjectsOfType<ButtonZone>();
        if (buttons != null)
        {
            float difference = 0;
            foreach (ButtonZone button in buttons)
            {
                difference = Vector3.Distance(transform.position, button.transform.position);
                if (difference < 1f)
                {
                    button.itemsInZone--;
                }
            }
        }
    }
}
