using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroicAIController : AIController
{
    // Get Some Player Stats
    private float targetCurrentHealth;
    private Vector3 targetPosition;
    private float targetAngle;

    // Public Vars
    public float amountVisionLengthExpanded;
    public float targetLowHealthThreshold;

    // Private Vars
    private bool expandVision = false;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        // Pull our Stats
        targetCurrentHealth = target.GetComponent<Health>().currentHealth;
        targetPosition = target.transform.position;
        targetAngle = Vector3.Angle(targetPosition, target.transform.forward);

        if (targetCurrentHealth <= targetLowHealthThreshold)
        {
            if (expandVision == false)
            {
                expandVision = true;
                visionLength = visionLength + amountVisionLengthExpanded;
            }
        }
        else
        {
            if (expandVision == true)
            {
                expandVision = false;
                visionLength = visionLength - amountVisionLengthExpanded;
            }
        }

        // Make Decisions
        MakeDecisions();
    }
}
