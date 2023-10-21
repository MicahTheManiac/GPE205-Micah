using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DumbAIController : AIController
{

    public float wanderCountdownSeconds;
    private float wanderCountdownTime;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        wanderCountdownTime = Time.time + wanderCountdownSeconds;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

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
                if (IsDistanceLessThan(target, detectionRadius))
                {
                    // And We See it
                    if (CanSee(target))
                    {
                        ChangeState(AIState.Chase);
                    }
                }

                // If it is time to Wander
                if (Time.time >= wanderCountdownTime)
                {
                    ChangeState(AIState.Wander);
                }

                break;

            case AIState.Chase:
                // Do Chase State
                DoChaseState();

                // If Target is out of Range
                if (!IsDistanceLessThan(target, detectionRadius))
                {
                    // And We can't See it
                    if (!CanSee(target))
                    {
                        ChangeState(AIState.Idle);
                    }
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
                    ChangeState(AIState.Wander);
                }
                break;

            case AIState.Patrol:
                // Do Patrol State
                DoPatrolState();

                // If Target is in Range
                if (IsDistanceLessThan(target, detectionRadius))
                {
                    // And We See it
                    if (CanSee(target))
                    {
                        ChangeState(AIState.Chase);
                    }
                }
                break;

            case AIState.Attack:
                // Do Attack State
                DoAttackState();

                // If Target is out of Range
                if (!IsDistanceLessThan(target, detectionRadius))
                {
                    // And We can't See it
                    if (!CanSee(target))
                    {
                        ChangeState(AIState.Wander);
                    }
                }
                // If We are Below Health Threshold
                if (IsHealthBelowThreshold())
                {
                    ChangeState(AIState.Flee);
                }
                break;

            case AIState.Wander:
                // Do Wander State
                DoWanderState();
                break;
        }
    }

    // Do Wander State
    public void DoWanderState()
    {
        Wander();
    }

    // Wander Function
    public void Wander()
    {
        // Get Target Distance
        float targetDistance = Vector3.Distance(target.transform.position, pawn.transform.position);

        // Make a "Random" Position Based on that
        float percentOffWanderingDistance = targetDistance / ( (fleeDistance * detectionRadius) / lowHealthThreshold + 5);
        float flippedPercentOffWanderingDistance = 1 - percentOffWanderingDistance;

        percentOffWanderingDistance = Mathf.Clamp01(percentOffWanderingDistance * flippedPercentOffWanderingDistance);

        // Find the Vector to our Target
        Vector3 vectorToTarget = target.transform.position - pawn.transform.position;

        // Find Vector away from our Target by Negating Vector
        Vector3 vectorAwayFromTarget = -vectorToTarget;

        // Find Vector to Travel Down (the Flee Vector)
        Vector3 wanderVector = vectorAwayFromTarget.normalized * percentOffWanderingDistance;

        // Seek the Flee Vector Point away from our Position
        Seek(pawn.transform.position + wanderVector);

        // Reset Timer
        wanderCountdownTime = Time.time + wanderCountdownSeconds;

    }

}
