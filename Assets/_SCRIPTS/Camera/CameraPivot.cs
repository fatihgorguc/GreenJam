using UnityEngine;
using UnityEngine.Serialization;

namespace _SCRIPTS.Camera
{
    public class CameraPivot : MonoBehaviour
    {
        [SerializeField] private Transform player;
        [SerializeField] private float moveSpeed = 5;
        
        //private Vector3 velocity = Vector3.zero;
        
        private void Update()
        {
            FollowTarget();
        }

        private void FollowTarget()
        {
            var targetPosition = player.position;
            targetPosition.y = transform.position.y;
            var newPosition = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime); 
            transform.position = newPosition;

            //transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, moveSpeed * Time.deltaTime);
        }
    }
}
