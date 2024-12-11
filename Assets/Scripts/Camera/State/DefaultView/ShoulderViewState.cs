using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UIElements;

public class ShoulderViewState : ICameraState
{
    public void UpdateState(CameraStateController CC)
    {
        /* if (CC.playerPhase)
         {
             Vector3 targetPosition = CC.target.position + CC.shoulderViewOffset;
             CC.transform.position = Vector3.Lerp(CC.transform.position, targetPosition, Time.deltaTime * CC.transitionSpeed);
             CC.transform.rotation = 
                 Quaternion.Lerp(CC.transform.rotation, 
                 Quaternion.Euler(CC.shoulderViewrotationOffset),
                 Time.deltaTime * CC.rotationSpeed);
         }
         else
         {
             Vector3 targetPosition = CC.target.position + CC.enemyShoulderViewOffset;
             CC.transform.position = Vector3.Lerp(CC.transform.position, targetPosition, Time.deltaTime * CC.transitionSpeed);
             CC.transform.rotation = 
                 Quaternion.Lerp(CC.transform.rotation, 
                 Quaternion.Euler(CC.enemyShoulderViewrotationOffset), 
                 Time.deltaTime * CC.rotationSpeed);
         }*/

        /*CC.transform.rotation =
                Quaternion.Lerp(CC.transform.rotation,
                Quaternion.Euler(CC.shoulderViewrotationOffset),
                Time.deltaTime * CC.rotationSpeed);*/

        CC.transform.LookAt(CC.lookTarget);
    }
}
