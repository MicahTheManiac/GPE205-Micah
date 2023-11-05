using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    public int searchForPlayerIndex = 0;
    public Vector3 offset = new Vector3(0, 20, -15);

    // Update is called once per frame
    void Update()
    {
        TargetPlayer(searchForPlayerIndex);
        transform.position = player.position + offset;
    }

    // Auto Set Target to Player One
    protected void TargetPlayer(int index)
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
                    // Then target the Tranform of the First Player Controller in the List
                    player = GameManager.instance.players[index].pawn.transform;
                }
            }
        }
    }
}
