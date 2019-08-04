using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ButtonType { Normal, Reverse, LockBehind };
public class ButtonZone : MonoBehaviour
{
    [SerializeField]
    GameObject doorObj;
    DoorMovement doorScript;
    [SerializeField]
    ButtonType buttonType;
    bool isActive;
    public AudioSource openSound, closeSound;
    public bool ActiveStatus
    {
        get
        {
            return isActive;
        }
    }

    //Number of objects on the button
    public int itemsInZone = 0;
    MeshRenderer meshRenderer;

    public delegate void ForceCheck();
    public ForceCheck stateCheck;
    //Material material;

    // Start is called before the first frame update
    void Awake()
    {
        doorScript = doorObj.GetComponent<DoorMovement>();
        //material = GetComponent<MeshRenderer>().material;
        meshRenderer = GetComponent<MeshRenderer>();
        if (buttonType == ButtonType.Reverse)
        {
            ReverseState();
            stateCheck += ReverseState;
        }
        else if (buttonType == ButtonType.Normal)
        {
            NormalState();
            stateCheck += NormalState;

        }
        else
        {
            ReverseState();
            stateCheck += ReverseState;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("Clone") || other.CompareTag("Player") || other.CompareTag("Enemy")))
        {
            EnterFuncion();
        }
    }

    public void EnterFuncion()
    {
        if (buttonType == ButtonType.Reverse)
        {
            itemsInZone--;
            ReverseState();
        }
        else if (buttonType == ButtonType.Normal)
        {
            itemsInZone++;

            NormalState();
        }
        else
        {
            LockState();
        }

    }

    public void ExitFunction()
    {
        if (buttonType == ButtonType.Reverse)
        {
            itemsInZone++;
            ReverseState();
        }
        else if (buttonType == ButtonType.Normal)
        {
            itemsInZone--;

            NormalState();
        }
        else
        {
            LockState();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((other.CompareTag("Clone") || other.CompareTag("Player") || other.CompareTag("Enemy")))
        {
            ExitFunction();
        }

    }

    void ReverseState()
    {
        if (itemsInZone == 0)
        {
            isActive = true;
            PlayAnimation(Color.green, openSound);
            //openSound.Play();
        }
        else
        {
            isActive = false;
            PlayAnimation(Color.red, closeSound);
            //closeSound.Play();

        }
    }

    void LockState()
    {
        //doorScript.CheckState();
        doorScript.MoveDoor(Vector3.zero);
        Destroy(this.gameObject);

    }

    void NormalState()
    {
        //Debug.Log("Entering: " + itemsInZone);
        if (itemsInZone > 0)
        {
            isActive = true;
            PlayAnimation(Color.green, openSound);
            //openSound.Play();
        }
        else
        {
            isActive = false;
            PlayAnimation(Color.red, closeSound);
            //closeSound.Play();
        }
    }

    void ButtonDeActive()
    {
        //Debug.Log("Exiting: " + itemsInZone);

        //if there's nothing on the button then change its active status to false.
        if (itemsInZone < 1)
        {
            isActive = false;
            PlayAnimation(Color.red, closeSound);
            //closeSound.Play();
        }
    }

    /// <summary>
    /// Takes a colour to change to and applies animation to material without update
    /// </summary>
    /// <param name="inColour"></param>
    /// <returns></returns>
    void PlayAnimation(Color inColour, AudioSource SoundToPlay)
    {
        Debug.Log("THINGS IN THE ZONE    " + itemsInZone);
        SoundToPlay.Play();
        doorScript.CheckState();
        GetComponent<MeshRenderer>().material.color = inColour;
    }
}
