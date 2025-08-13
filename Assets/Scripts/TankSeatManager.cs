using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class TankSeatManager : NetworkBehaviour
{

    // todo: attempting to move to move to system that stores the players and seats in a networklist and on networklistchange all other places are notified
    // todo: rename seats and 

    // check notes on the pc for how to do this

    private Dictionary<int, TankSeat> m_tankSeatComponentDict = new();
    // Stores seatnumbers corresponding to PlayerIds inside the tank
    private Dictionary<int, ulong?> m_tankSeatPlayerDict = new();

    private void Awake()
    {
        // Finding all of the tankseat script components in the tankseat gameobjects which are children of this object
        TankSeat[] childSeats = GetComponentsInChildren<TankSeat>();

        // Iterating through the seat scripts found and adding them to the dictionary
        foreach (var seat in childSeats)
        {
            m_tankSeatComponentDict.Add(seat.seatNumber, seat);
            m_tankSeatPlayerDict.Add(seat.seatNumber, null); // Initalizing the playerId in each seat to null
        }
    }

    public TankSeat GetSeatBySeatNumber(int number)
    {
        if (m_tankSeatComponentDict.TryGetValue(number, out TankSeat seat))
        {
            return seat;
        }

        Debug.LogWarning($"No seat found with number {number}");
        return null;
    }


    /// <summary>
    /// Server RPC to request the specified player to enter a specified seat
    /// Called by the client, but runs on the server
    /// </summary>
    /// <param name="playerId">Player ID as stored by the NetworkManager</param>
    /// <param name="seatNumber">Specified seatnumber to enter (1,2,3,4)</param>
    [ServerRpc(RequireOwnership = false)]
    private void RequestEnterSeatServerRpc(ulong playerId, int seatNumber)
    {
        // If an invalid seatnumber is passed in
        if (!m_tankSeatComponentDict.ContainsKey(seatNumber))
        {
            Debug.LogWarning($"Invalid seatnumber: {seatNumber} passed as a parameter");
            return;
        }

        // Check to see if the tankseat is already occupied
        if (m_tankSeatPlayerDict[seatNumber] != null)
        {
            Debug.LogWarning($"Seat {seatNumber} is already occupied by playerId: ${m_tankSeatPlayerDict[seatNumber]}");
        }

        // Assign the playerId to the seat
        m_tankSeatPlayerDict[seatNumber] = playerId;

        // Parent the seat's transform to the player
        NetworkObject playerObj = NetworkManager.Singleton.SpawnManager.GetPlayerNetworkObject(playerId); // Todo double check everything here
        if (playerObj != null)
        {
            playerObj.transform.SetParent(m_tankSeatComponentDict[seatNumber].transform); // Parenting occurs here

            // Position allignment occurs below
            playerObj.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning($"Player with playerId: ${playerId} could not be found by the NetworkManager");
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

    // [Rpc(SendTo.Server)]
    // public void PlayerExitSeatRpc(int seatNumber)
    // {
    //     // get the seat 
    //     // unparent the person inside the seat
    //     isSeatOccupied.Value = false;
    // }

    // [Rpc(SendTo.Server)]
    // public void PlayerEnterSeatRpc(int seatNumber, int playerId)
    // {
    //     // use the parenting logic here to happen?
    //     isSeatOccupied.Value = true;
    //     m_occupiedPlayerId = playerId;
    // }
}