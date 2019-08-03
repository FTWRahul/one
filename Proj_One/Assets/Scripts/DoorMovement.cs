using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class DoorMovement : MonoBehaviour
{
    public List<ButtonZone> Buttons;

    [SerializeField]
    GameObject moveWaypoint;

    Sequence doorSequence;
    public AudioSource doorSound;

    public void CheckState()
    {
        int k = 0;
        for (int i = 0; i < Buttons.Count; i++)
        {
            //if button is active increase the count
            if (!Buttons[i].ActiveStatus)
            {
                k--;
            }
            else
            {
                k++;
            }
        }
        //If count is the same as the number of buttons then open the door.
        if (k == Buttons.Count)
        {
            if (transform.localPosition != moveWaypoint.transform.localPosition)
            {
                MoveDoor(moveWaypoint.transform.localPosition);
            }
        }
        else
        {
            if (transform.localPosition != Vector3.zero)
            {
                MoveDoor(Vector3.zero);
            }
        }
    }

    public void MoveDoor(Vector3 pos)
    {
        doorSound.PlayDelayed(.35f);
        doorSequence = DOTween.Sequence();
        doorSequence.Complete();

        transform.DOLocalMove(pos, 1f).SetEase(Ease.OutBounce);
    }
}
