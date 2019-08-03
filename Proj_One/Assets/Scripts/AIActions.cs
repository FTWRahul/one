using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AIActions
{
    public PatrolActions patrolActions;
    public Rotation rotationEnum;
    public GameObject patrolPositions;
    public float time;

    public AIActions(PatrolActions patrolActions, Rotation rotationEnum, GameObject patrolPositions, float time)
    {
        this.patrolActions = patrolActions;
        this.rotationEnum = rotationEnum;
        this.patrolPositions = patrolPositions;
        this.time = time;
    }
}
