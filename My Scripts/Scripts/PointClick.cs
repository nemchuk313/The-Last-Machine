using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PointCLick : MonoBehaviour
{

    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _rotationSpeed;
    [SerializeField]
    private CharacterController characterController;

    
    private Vector3 _position;
    //RayCast distance
    private float _distance = 1000;

    // Start is called before the first frame update
    void Start()
    {
        _position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetMouseButton(1))
        {
            //Locate where the player clicked on the terrain
            LocatePosition();
        }

        MoveToPosition();

    }

    private void LocatePosition()
    {
        RaycastHit hit;//allows to get info from "Ray"

        //Create a  ray line point from camera to the mouse position int the game 
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //Cast the ray and gives all the info to "hit" var
        if(Physics.Raycast(ray , out hit , _distance))
        {
            //Store the position where Ray interssects with a terrain to "_position"
            _position = new Vector3(hit.point.x , hit.point.y , hit.point.z);
            Debug.Log(_position);
        }
    }

    private void MoveToPosition()
    {
        //Move only if the player hasn't reached a destination yet

        if(Vector3.Distance(transform.position,_position) > 17)
        {
           // Store the direction player will rotate to
            Quaternion newRotation = Quaternion.LookRotation(_position - transform.position);

            newRotation.x = 0f;
            newRotation.z = 0f;

            //Rotate the player towards click point
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * _rotationSpeed);

            //Move the player
            characterController.SimpleMove(transform.forward * _speed);
        }
    }
}
 