using Unity.Netcode;
using UnityEngine;

public class TankSeat : NetworkBehaviour
{
    public int seatNumber;
    public Transform SeatTransform => transform;
}
