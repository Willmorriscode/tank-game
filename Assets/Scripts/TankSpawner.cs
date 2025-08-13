using Unity.Netcode;
using UnityEngine;

public class TankSpawner : NetworkBehaviour
{

    // this file should really be on the tank itself but for testing purposes we will put it on the character
    public GameObject TankPrefab;
    private GameObject m_TankPrefabInstance;
    private NetworkObject m_SpawnedNetworkTank;

    // private void Update()
    // {
    //     if (!IsOwner) return;

    //     if (Input.GetKeyDown(KeyCode.T))
    //     {
    //         Transform m_spawnedObjectTransform = Instantiate(m_spawnedObjectPrefab);
    //         m_spawnedObjectTransform.GetComponent<NetworkObject>().Spawn(true);
    //     }
    // }

    public override void OnNetworkSpawn()
    {
        // spawns the tank only on the server
        SpawnTankRpc();
    }

    [Rpc(SendTo.Server)]
    private void SpawnTankRpc()
    {
        // Instantiate the GameObject Instance
        m_TankPrefabInstance = Instantiate(TankPrefab);

        // Get the instance's NetworkObject and Spawn
        m_SpawnedNetworkTank = m_TankPrefabInstance.GetComponent<NetworkObject>();
        m_SpawnedNetworkTank.Spawn();
    }
}

