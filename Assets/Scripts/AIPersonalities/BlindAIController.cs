using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlindAIController : AIController
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
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
                if (IsDistanceLessThan(target, hearingDistance))
                {
                    // And We See it
                    if (CanHear(target))
                    {
                        ChangeState(AIState.Chase);
                    }
                }
                break;

            case AIState.Chase:
                // Do Chase State
                DoChaseState();

                // If Target is out of Range
                if (!IsDistanceLessThan(target, hearingDistance))
                {
                    // And We can't See it
                    if (!CanHear(target))
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
                    ChangeState(AIState.Idle);
                }
                break;

            case AIState.Patrol:
                // Do Patrol State
                DoPatrolState();

                // If Target is in Range
                if (IsDistanceLessThan(target, hearingDistance))
                {
                    // And We See it
                    if (CanHear(target))
                    {
                        ChangeState(AIState.Chase);
                    }
                }
                break;

            case AIState.Attack:
                // Do Attack State
                DoAttackState();

                // If Target is out of Range
                if (!IsDistanceLessThan(target, hearingDistance))
                {
                    // And We can't See it
                    if (!CanHear(target))
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
        }
    }
}
