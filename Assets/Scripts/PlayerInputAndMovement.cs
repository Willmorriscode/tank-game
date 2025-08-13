using Unity.Netcode;
using UnityEngine;

public class PlayerInputAndMovement : NetworkBehaviour
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

        PlayerMovement();
        PlayerTankControls();
    }

    private void PlayerTankControls()
    {
        if (Input.GetKey(KeyCode.Keypad1))
        {
            // call server rpc to enter seat 1 here
        }

        if (Input.GetKey(KeyCode.Keypad2))
        {
            // call server rpc to enter seat 2 here
        }

        if (Input.GetKey(KeyCode.Keypad3))
        {
            // call server rpc to enter seat 3 here
        }

        if (Input.GetKey(KeyCode.Keypad4))
        {
            // call server rpc to enter seat 3 here
        }
    }

    private void PlayerMovement()
    {
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
