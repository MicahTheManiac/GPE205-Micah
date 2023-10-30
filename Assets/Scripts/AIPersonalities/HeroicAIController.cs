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
    public float amountDetectionRadiusExpanded;
    public float targetLowHealthThreshold;

    // Private Vars
    private bool expandDetectionRadius = false;

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
            if (expandDetectionRadius == false)
            {
                expandDetectionRadius = true;
                detectionRadius = detectionRadius + amountDetectionRadiusExpanded;
            }
        }
        else
        {
            if (expandDetectionRadius == true)
            {
                expandDetectionRadius = false;
                detectionRadius = detectionRadius - amountDetectionRadiusExpanded;
            }
        }

        // Make Decisions
        MakeDecisions();
    }

    // Make Decisions
    new void MakeDecisions()
    {
        switch (currentState)
        {
            case AIState.Idle:
                // Do Idle State
                DoIdleState();

                // If Target is in Range
                if (CanSee(target))
                {
                    ChangeState(AIState.Chase);
                }
                break;

            case AIState.Chase:
                // Do Chase State
                DoChaseState();

                // If Target is out of Range
                if (!CanSee(target))
                {
                    ChangeState(AIState.Idle);
                }
                // If We are Below Health Threshold
                if (IsHealthBelowThreshold())
                {
                    ChangeState(AIState.Flee);
                }
                break;

            case AIState.Flee:
                // Do Flee State
                DoFleeState();

                // Check to see if We are far enough. Don't worry about Seeing Target
                if (!IsDistanceLessThan(target, fleeDistance))
                {
                    ChangeState(AIState.Idle);
                }
                break;

            case AIState.Patrol:
                // Do Patrol State
                DoPatrolState();

                // If Target is in Range
                if (CanSee(target))
                {
                    ChangeState(AIState.Chase);
                }
                break;

            case AIState.Attack:
                // Do Attack State
                DoAttackState();

                // If Target is out of Range
                if (!CanSee(target))
                {
                    ChangeState(AIState.Chase);
                }
                // If We are Below Health Threshold
                if (IsHealthBelowThreshold())
                {
                    ChangeState(AIState.Flee);
                }
                break;
        }
    }
}
