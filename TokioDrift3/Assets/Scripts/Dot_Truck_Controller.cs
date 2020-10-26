using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

[System.Serializable]
public class Dot_Truck : System.Object
{
	public WheelCollider leftWheel;
	public GameObject leftWheelMesh;
	public WheelCollider rightWheel;
	public GameObject rightWheelMesh;
    public bool motor;
	public bool steering;
	public bool reverseTurn; 
}

public class Dot_Truck_Controller : MonoBehaviour {

   
	public List<Dot_Truck> truck_Infos;
	public float maxMotorTorque;
	public float maxSteeringAngle;
    //public GameObject Timon;
    string recognizedText;
	int current = 0;
    float moveSpeed = 10.0f;
	float boost = 200.0f;
	public GameObject turbo;
	Rigidbody rb;
	float topSpeed = 20.0f;

	public void VisualizeWheel(Dot_Truck wheelPair)
	{
		Quaternion rot;
		Vector3 pos;
		wheelPair.leftWheel.GetWorldPose ( out pos, out rot);
		wheelPair.leftWheelMesh.transform.position = pos;
		wheelPair.leftWheelMesh.transform.rotation = rot;
		wheelPair.rightWheel.GetWorldPose ( out pos, out rot);
		wheelPair.rightWheelMesh.transform.position = pos;
		wheelPair.rightWheelMesh.transform.rotation = rot;
	}
	public void Start(){
		rb= GetComponent<Rigidbody>();
	}
	public void Update()
    {
		/*if(Mathf.Round(rb.velocity.magnitude)>topSpeed)
		{
			turbo.SetActive(true);
		}
		else{
			turbo.SetActive(false);
		}*/
        //Vector3 position = transform.position;
		//float tempX = (Input.acceleration.x*moveSpeed);
		//para manejar con movimientos de llantas
        //float tempY = (Input.acceleration.y);
        //float motor = maxMotorTorque * tempY;
        //float steering = maxSteeringAngle * tempX;
        //Debug.Log(tempX);


        float motor = maxMotorTorque * Input.GetAxis("Vertical");
        float steering = maxSteeringAngle * Input.GetAxis("Horizontal");
		//usando con lo de arriba para mejor manejo
        float brakeTorque = Mathf.Abs(Input.GetAxis("Jump"));
		if (brakeTorque > 0.1) {
			Debug.Log (brakeTorque);
			brakeTorque = maxMotorTorque;
			motor = 0;
		} else {
			brakeTorque = 0;
		}
		foreach (Dot_Truck truck_Info in truck_Infos)
		{
			if (truck_Info.steering == true) {
				truck_Info.leftWheel.steerAngle = truck_Info.rightWheel.steerAngle = ((truck_Info.reverseTurn)?-1:1)*steering;
                //Timon.transform.Rotate(0, 0, steering*(-0.3f));
            }

			if (truck_Info.motor == true)
			{
				truck_Info.leftWheel.motorTorque = motor;
				truck_Info.rightWheel.motorTorque = motor;
			}
			

			truck_Info.leftWheel.brakeTorque = brakeTorque;
			truck_Info.rightWheel.brakeTorque = brakeTorque;

			//VisualizeWheel(truck_Info);

		}
		
				if(Input.GetKeyDown(KeyCode.F))
				{
					var force = rb.transform.forward * boost;
					rb.AddForce(force,ForceMode.Acceleration);
					turbo.SetActive(true);
					//new WaitForSeconds(5);
					//turbo.SetActive(false);
					Debug.Log(transform.forward);
				}
				if(Input.GetKeyUp(KeyCode.F))
				{
					turbo.SetActive(false);
				}
		
		
			
			

		
	}

	/*IEnumerator Text()  //  <-  its a standalone method
	{
		
    	yield return new WaitForSeconds(3);
    	
	}*/

	
}