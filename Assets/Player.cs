using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    // Pointer to the player game object's transform component.
    Transform pTransform;
    // Pointer to the player game object's character controller component.
    CharacterController pController;
    // Player movement speed.
    float movSpeed = 8.0f;
    // Gravity.
    float gravity = 2.0f;
    // Health.
    int health = 5;

    // Camera Transform
    Transform cTransform;

    // Camera Rotation Angle
    Vector3 cRotation;

    // Camera Height
    float cHeight = 1.5f;


    // Start is called before the first frame update
    void Start()
    {
        pTransform = this.transform;
        pController = this.GetComponent<CharacterController>();
        cTransform = Camera.main.transform;
        // Calculate the position for the camera.
        Vector3 pos = pTransform.position;
        pos.y += cHeight;
        cTransform.position = pos;
        // Calculate and set the rotation for the camera.
        cTransform.rotation = pTransform.rotation;
        cRotation = cTransform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.health <= 0) return;
        moveCharacter();
    }


    void moveCharacter()
    {
        // deltaX, deltaY, deltaZ
        float x = 0, y = 0, z = 0;
        // Update movement along y-axis
        y -= gravity * Time.deltaTime;

        if (Input.GetKey(KeyCode.W))
        {
            z += movSpeed * Time.deltaTime;
        } else if (Input.GetKey(KeyCode.S))
        {
            z -= movSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.A))
        {
            x -= movSpeed * Time.deltaTime;
        } else if (Input.GetKey(KeyCode.D))
        {
            x += movSpeed * Time.deltaTime;
        }

        float mx = Input.GetAxis("Mouse X");
        float my = Input.GetAxis("Mouse Y");

        // Calculate Euler Angle.
        cRotation.x -= my;
        cRotation.y += mx;
        // Set new rotation.
        cTransform.eulerAngles = cRotation;

        // Set player rotation angle.
        Vector3 rot = cTransform.eulerAngles;
        rot.x = 0;
        rot.z = 0;
        pTransform.eulerAngles = rot;


        this.pController.Move(pTransform.TransformDirection(new Vector3(x, y, z)));
        // Set camera position to be player's position.
        Vector3 pos = pTransform.position;
        pos.y += cHeight;
        cTransform.position = pos;
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawIcon(this.transform.position, "Spawn.tif");
    }
}

