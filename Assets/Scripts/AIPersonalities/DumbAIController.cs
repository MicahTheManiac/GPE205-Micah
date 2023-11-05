using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DumbAIController : AIController
{
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
                if (CanSee(target, fieldOfView))
                {
                    ChangeState(AIState.Chase);
                }

                // If Target is in LoS
                if (CanSee(target, lineOfSightAngle))
                {
                    ChangeState(AIState.Attack);
                }

                // If it is time to Wander
                if (Time.time >= wanderCountdownTime)
                {
                    // Reset Timer
                    wanderCountdownTime = Time.time + wanderCountdownSeconds;

                    // Change State
                    ChangeState(AIState.Wander);
                }

                break;

            case AIState.Chase:
                // Do Chase State
                DoChaseState();

                // If Target is out of Range
                if (!CanSee(target, fieldOfView))
                {
                    ChangeState(AIState.Idle);
                }

                // If Target is in LoS
                if (CanSee(target, lineOfSightAngle))
                {
                    ChangeState(AIState.Attack);
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
                if (CanSee(target, fieldOfView))
                {
                    ChangeState(AIState.Chase);
                }

                // If Target is in LoS
                if (CanSee(target, lineOfSightAngle))
                {
                    ChangeState(AIState.Attack);
                }
                break;

            case AIState.Attack:
                // Do Attack State
                DoAttackState();

                // If Target is out of Range
                if (!CanSee(target, fieldOfView))
                {
                    ChangeState(AIState.Idle);
                }

                // If Target is in LoS
                if (CanSee(target, lineOfSightAngle))
                {
                    ChangeState(AIState.Attack);
                }
                break;

            case AIState.Wander:
                // Do Wander State
                DoWanderState();
                break;
        }
    }
}
