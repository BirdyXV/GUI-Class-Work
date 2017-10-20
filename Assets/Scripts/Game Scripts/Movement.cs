using UnityEngine;
using System.Collections;
//this script can be found in the Component section under the option Character Set Up 
//Character Movement
[AddComponentMenu("Character Set Up/Character Movement")]
//This script requires the component Character controller
[RequireComponent(typeof(CharacterController))]
public class Movement : MonoBehaviour
{
    #region Variables
    [Header("Characters MoveDirection")]
    // We will use this to apply movement in worldspace
    public Vector3 moveDir;
    // Private CharacterController (https://docs.unity3d.com/ScriptReference/CharacterController.html) charC
    private CharacterController charC;

    [Header("Character Variables")]
    public float jumpSpeed;
    public float speed, gravity;
    #endregion
    #region Start
    void Start()
    {
        charC = GetComponent<CharacterController>();
    }
    #endregion
    #region Update
    void Update()
    {
        // Checking if we are grounded
        if (charC.isGrounded)
        {
            // Setting up our axis and speed
            moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDir = transform.TransformDirection(moveDir);
            moveDir *= speed;
        }

        // Checking if we have pressed "Jump"
        if (Input.GetButton("Jump"))
        {
            // Jump
            moveDir.y = jumpSpeed;
        }
        // Multiplying the jump by deltaTime to normalize it
        // Telling the character it is moving in a direction multiplied by deltaTime
        moveDir.y -= gravity * Time.deltaTime;
        charC.Move(moveDir * Time.deltaTime);
    }
    #endregion
}














