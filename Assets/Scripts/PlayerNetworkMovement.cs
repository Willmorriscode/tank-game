using Unity.Netcode;
using UnityEngine;

public class PlayerNetwork : NetworkBehaviour
{
    // useful for later
    /*
        networkVar.OnValueChanged += (T prevValue, T newValue) =>  { }

    // OwnerClientId will give you that client's ID, that will be useful for tracking tank seats 

    [ServerRpc] // when called it will run on the server client only
    private void exampleRpc(){
    }
    
    call exampleRpc like normal, its just a function with the location specified differently

    */

    private void Update()
    {
        if (!IsOwner) return; // only run code to move gameobject when you own the object

        // basic movement control for wsad controls
        Vector3 m_moveDir = new(0, 0, 0);

        // forward
        if (Input.GetKey(KeyCode.W))
        {
            m_moveDir.z = +1f;
        }

        // backwards
        if (Input.GetKey(KeyCode.S))
        {
            m_moveDir.z = -1f;
        }

        // left
        if (Input.GetKey(KeyCode.A))
        {
            m_moveDir.x = -1f;
        }

        // right
        if (Input.GetKey(KeyCode.D))
        {
            m_moveDir.x = +1f;
        }

        float m_moveSpeed = 3f;
        transform.position += m_moveSpeed * Time.deltaTime * m_moveDir;
    }
}
