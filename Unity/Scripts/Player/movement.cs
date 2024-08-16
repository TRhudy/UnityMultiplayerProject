using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    float movementSpeed = 5.0f;

    private void Update()
    {
        Vector3 moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 newMoveDir = moveDir * movementSpeed;
        transform.position += newMoveDir * Time.deltaTime;
    }

}