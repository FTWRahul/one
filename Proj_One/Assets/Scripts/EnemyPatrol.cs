using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum PatrolActions {Stand, Move, Rotate}

public class EnemyPatrol : MonoBehaviour
{
    PatrolActions actions;
    Dictionary<PatrolActions, string> PatrolDictionary; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //PatrolDictionary.a
    }
}
