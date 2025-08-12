using Unity.Netcode;
using UnityEngine;

public class TankNetworkSeatManager : NetworkBehaviour
{

    // todo: attempting to move to move to system that stores the players and seats in a networklist and on networklistchange all other places are notified
    // todo: rename seats and 

    // check notes on the pc for how to do this

    private int m_occupiedPlayerId = -1;
    public NetworkVariable<bool> isSeatOccupied = new(false);

    public override void OnNetworkSpawn()
    {
        // adding subscribers to watch for when the seat becomes occupied 
        isSeatOccupied.OnValueChanged += OnSeatOccupiedChange;
    }

    public override void OnNetworkDespawn()
    {
        // removing subscribers for when the tank is despawned
        isSeatOccupied.OnValueChanged -= OnSeatOccupiedChange;
    }


    public void OnSeatOccupiedChange(bool previous, bool current)
    {
        // executes if the seat has become occupied
        if (isSeatOccupied.Value)
        {
            // use occupiedPlayerId.Value here to parent to the seat

            AttachPlayerToSeat();

            // seat should now be occipied, parent a player to it and / or teleport them to the seat
            // Activate the UI that is appropriate for their seat
        }

        // executes if a player leaves a seat
        else
        {
            // Un-parent the player from this seat
            // remove the changed hud appropriate for the seat

            DetachPlayerFromSeat();
        }
    }

    private void AttachPlayerToSeat()
    {
        // parent to seat
        // adjust transforms to be in the seat
    }

    private void DetachPlayerFromSeat()
    {
        // un-parent from seat
        // place the transforms outsidet the tank
    }

    [Rpc(SendTo.Server)]
    public void PlayerExitSeatRpc(int seatNumber)
    {
        // get the seat 
        // unparent the person inside the seat
        isSeatOccupied.Value = false;
    }

    [Rpc(SendTo.Server)]
    public void PlayerEnterSeatRpc(int seatNumber, int playerId)
    {
        // use the parenting logic here to happen?
        isSeatOccupied.Value = true;
        m_occupiedPlayerId = playerId;
    }
}



///// overhaul code below 
/// 
/// 
/// 
/// 