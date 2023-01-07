using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKFootPlacement : MonoBehaviour
{
    Animator animator;
    public LayerMask layerMask;
    [Range(0,1)] public float distanceToGround;
    private void Start() 
    {
        animator = GetComponent<Animator>();
    }

    private void OnAnimatorIK(int layerIndex) 
    {
        if(animator)
        {

            animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot,1f);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot,1f);

            animator.SetIKPositionWeight(AvatarIKGoal.RightFoot,1f);
            animator.SetIKRotationWeight(AvatarIKGoal.RightFoot,1f);

            RaycastHit hit;
            Ray ray = new Ray(animator.GetIKPosition(AvatarIKGoal.LeftFoot) + Vector3.up, Vector3.down);
            if(Physics.Raycast(ray,out hit,distanceToGround + 1f, layerMask))
            {
                if(hit.transform.tag == "Testable")
                {
                    Vector3 footPosition = hit.point;
                    footPosition.y += distanceToGround;
                    animator.SetIKPosition(AvatarIKGoal.LeftFoot,footPosition);
                    Vector3 forward = Vector3.ProjectOnPlane(transform.forward, hit.normal);
                    animator.SetIKRotation(AvatarIKGoal.LeftFoot, Quaternion.LookRotation(forward,hit.normal));
                }
                
            }

            ray = new Ray(animator.GetIKPosition(AvatarIKGoal.RightFoot) + Vector3.up, Vector3.down);
            if(Physics.Raycast(ray,out hit,distanceToGround + 1f, layerMask))
            {
                if(hit.transform.tag == "Testable")
                {
                    Vector3 footPosition = hit.point;
                    footPosition.y += distanceToGround;
                    animator.SetIKPosition(AvatarIKGoal.RightFoot,footPosition);
                    Vector3 forward = Vector3.ProjectOnPlane(transform.forward, hit.normal);
                    animator.SetIKRotation(AvatarIKGoal.RightFoot, Quaternion.LookRotation(forward,hit.normal)); 
                }
            }


        }
    }
}
