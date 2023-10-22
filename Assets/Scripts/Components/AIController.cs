using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : Controller
{
    public enum AIState { Idle, Chase, Flee, Attack, Patrol, Wander };
    public AIState currentState;
    public GameObject target;
    public bool doLooping = true;
    public float lowHealthThreshold = 15.0f;

    // Distance Vars.
    public float detectionRadius = 20.0f;
    public float fleeDistance;
    public float hearingDistance;
    public float fieldOfView;

    // Waypoints
    public Transform[] waypoints;
    public float waypointStopDistance;
    private int currentWaypoint = 0;

    private float lastStateChangeTime;

    // Is Distance Less Than Bool
    protected bool IsDistanceLessThan(GameObject target, float distance)
    {
        if (Vector3.Distance(pawn.transform.position, target.transform.position) < distance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // Check to see if We have a Target
    protected bool IsHasTarget()
    {
        // Return True if we Do, False if we Don't
        return (target != null);
    }

    // Check to See if we can hear
    public bool CanHear(GameObject target)
    {
        // Get the target's NoiseMaker
        NoiseMaker noiseMaker = target.GetComponent<NoiseMaker>();

        // If they Don't have one Return False. They can't make noise
        if (noiseMaker == null)
        {
            return false;
        }

        // If they are making 0 noise Return False
        if (noiseMaker.volumeDistance <= 0)
        {
            return false;
        }

        // If they are making noise add the volumeDistance of the NoiseMaker to the hearingDistance in this AI
        float totalDistance = noiseMaker.volumeDistance + hearingDistance;

        // If the distance between our Pawn and the Target is closer than totalDistance
        if (Vector3.Distance(pawn.transform.position, target.transform.position) <= totalDistance)
        {
            // If they're making noise
            if (noiseMaker.makingNoise == true)
            {
                // Then Return True. We hear the target
                return true;
            }

            return false;
        }
        else
        {
            // Otherwise Return False. We are too far away
            return false;
        }

    }

    // Check to see if We can See
    protected bool CanSee(GameObject target)
    {
        // Find the Vector from Agent (Us) to the Target
        Vector3 agentToTargetVector = target.transform.position - pawn.transform.position;

        // Find the Angle between Direction We are facing and the Vector to the Target
        float angleToTarget = Vector3.Angle(agentToTargetVector, pawn.transform.forward);

        // If that Angle is Less Than our Field of View
        if (angleToTarget < fieldOfView)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    // Check our Health
    public bool IsHealthBelowThreshold()
    {
        // Store our Health Component
        Health health = pawn.GetComponent<Health>();

        // Check to see if Component is Valid
        if (health != null)
        {
            if (health.currentHealth < lowHealthThreshold)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    // Start is called before the first frame update
    public override void Start()
    {
        // Check for Pawn
        CheckForPawn();

        // Set Beginning State
        ChangeState(AIState.Idle);

        // Run Parent Start
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        // Check for Pawn
        CheckForPawn();

        // Check for Target
        if (!IsHasTarget())
        {
            TargetNearestPlayer();
        }

        // Make AI Decisions -- Should be Reserved for Personalitites
        // MakeDecisions();

        // Run Parent Update
        base.Update();
    }

    // Override ProcessInputs
    public override void ProcessInputs()
    {
        
    }

    // Check to See if Pawn is null or not.
    public void CheckForPawn()
    {
        if (pawn == null)
        {
            Debug.Log(gameObject.name + ": Self Destruct, I am missing my Pawn!");
            Destroy(gameObject);
            
        }
    }

    // Auto Set Target to Player One
    protected void TargetPlayerOne()
    {
        // If the GameManager exists
        if (GameManager.instance != null)
        {
            // And the Player Array exists
            if (GameManager.instance.players != null)
            {
                // And there are Players in it
                if (GameManager.instance.players.Count > 0)
                {
                    // Then target the gameObject of the First Player Controller in the List
                    target = GameManager.instance.players[0].pawn.gameObject;
                }
            }
        }
    }

    // Target Nearest Tank
    protected void TargetNearestTank()
    {
        // Get a list of all Tanks (Pawns)
        Pawn[] allTanks = FindObjectsOfType<Pawn>();

        // Assume the First Tank is Closest
        Pawn closestTank = allTanks[0];
        float closestTankDistance = Vector3.Distance(pawn.transform.position, closestTank.transform.position);

        // Iterate through one at a time
        foreach( Pawn tank in allTanks)
        {
            if (Vector3.Distance(pawn.transform.position, closestTank.transform.position) <= closestTankDistance)
            {
                // It is the Closest
                closestTank = tank;
                closestTankDistance = Vector3.Distance(pawn.transform.position, closestTank.transform.position);

            }
        }

        // Target the Closest Tank
        target = closestTank.gameObject;
    }

    // Target Nearest Player
    protected void TargetNearestPlayer()
    {
        // If the GameManager exists
        if (GameManager.instance != null)
        {
            // And the Player Array exists
            if (GameManager.instance.players != null)
            {
                // And there are Players in it
                if (GameManager.instance.players.Count > 0)
                {
                    // Get a List of all Players (PlayerControllers)
                    PlayerController[] allPlayers = FindObjectsOfType<PlayerController>();

                    // Assume the First Tank is Closest
                    Pawn closestPlayer = allPlayers[0].pawn;
                    float closestPlayerDistance = Vector3.Distance(pawn.transform.position, closestPlayer.transform.position);

                    // Iterate through one at a time
                    foreach (PlayerController player in allPlayers)
                    {
                        if (Vector3.Distance(pawn.transform.position, closestPlayer.transform.position) <= closestPlayerDistance)
                        {
                            // It is the Closest
                            closestPlayer = player.pawn; // Suffix '.pawn' is added since closestPlayer is of Pawn type
                            closestPlayerDistance = Vector3.Distance(pawn.transform.position, closestPlayer.transform.position);

                        }
                    }

                    // Target the Closest PLayer
                    target = closestPlayer.gameObject; // Suffix '.pawn.gameObject' is not needed since we already store the pawn
                }
            }
        }
    }

    // Make Decisions
    public void MakeDecisions()
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
                    ChangeState(AIState.Idle);
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

    public void Shoot()
    {
        // Tell the Pawn to Shoot
        pawn.Shoot();
    }


    // Idle State
    protected virtual void DoIdleState()
    {
        // Do Nothing
    }

    // Seeking State
    public void DoSeekState()
    {
        // Seek our Target
        Seek(target);
    }
    
    // Chasing State
    protected virtual void DoChaseState()
    {
        Seek(target);
    }

    // Attacking State
    protected virtual void DoAttackState()
    {
        // Chase
        Seek(target);
        // Shoot
        Shoot();
    }

    // Fleeing State
    protected virtual void DoFleeState()
    {
        Flee();
    }

    protected virtual void DoPatrolState()
    {
        Patrol();
    }


    // Patrol Function
    protected void Patrol()
    {

        // If we have enough Waypoints in out list to move to a current Waypoint
        if (waypoints.Length > currentWaypoint)
        {
            // Seek to that Waypoint
            Seek(waypoints[currentWaypoint]);
            // If we are close enough, then increment to Next Waypoint
            if (Vector3.Distance(pawn.transform.position, waypoints[currentWaypoint].position) < waypointStopDistance)
            {
                currentWaypoint++;
            }
        }
        else
        {
            if (doLooping)
            {
                // Restart the Patrol
                RestartPatrol();
            }
            else
            {
                // Restart the Patrol, BUT...
                RestartPatrol();
                // Change State
                ChangeState(AIState.Idle);
            }
        }
    }

    // Restart Patrol Function
    protected void RestartPatrol()
    {
        // Set Index to 0
        currentWaypoint = 0;
    }

    // Flee Function
    protected void Flee()
    {
        // Variables
        float targetDistance = Vector3.Distance(target.transform.position, pawn.transform.position);
        float percentOffFleeDistance = targetDistance / fleeDistance;
        float flippedPercentOffFleeDistance = 1 - percentOffFleeDistance;

        percentOffFleeDistance = Mathf.Clamp01(percentOffFleeDistance * flippedPercentOffFleeDistance);

        // Find the Vector to our Target
        Vector3 vectorToTarget = target.transform.position - pawn.transform.position;

        // Find Vector away from our Target by Negating Vector
        Vector3 vectorAwayFromTarget = -vectorToTarget;

        // Find Vector to Travel Down (the Flee Vector)
        Vector3 fleeVector = vectorAwayFromTarget.normalized * percentOffFleeDistance;

        // Seek the Flee Vector Point away from our Position
        Seek(pawn.transform.position + fleeVector);
    }

    // Seek Function -- Rotate and Move
    public void Seek(Vector3 targetPosition)
    {
        // RotateTowards the Target
        pawn.RotateTowards(targetPosition);

        // Move Forward
        pawn.MoveForward();
    }

    // Seek Function -- Target Transform
    public void Seek(Transform targetTransform)
    {
        // Seek the Position of out Target Transform
        Seek(targetTransform.position);
    }

    // Seek Function -- Pawn Transform
    public void Seek(Pawn targetPawn)
    {
        // Seek the Pawn's Transform
        Seek(targetPawn.transform);
    }

    // Seek Function -- A Controller's Pawn
    public void Seek (Controller controller)
    {
        // Seek the Controller Pawn's Tranform
        Seek(controller.pawn);
    }

    // Seek Function -- Game Object's Transform
    public void Seek(GameObject gameObject)
    {
        // Seek the Controller Pawn's Tranform
        Seek(gameObject.transform);
    }


    // Change State
    public virtual void ChangeState(AIState newState)
    {
        // Change Current State
        currentState = newState;
        // Save the Time when we Changed States
        lastStateChangeTime = Time.time;

    }

    
}
