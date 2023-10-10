using System;
using Scripts.Player.Manager;
using UnityEngine;

namespace CameraSystem
{
    public class CameraFade : MonoBehaviour
    {
        private FadingObject fadingObject;
        private void Update()
        {
            Vector3 playerPos = new Vector3(PlayerManager.Instance.transform.position.x,
                PlayerManager.Instance.transform.position.y + 0.5f, PlayerManager.Instance.transform.position.z);
            
            Vector3 direction = playerPos - transform.position;
            Ray ray = new Ray(transform.position, direction);

            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                Debug.DrawLine(ray.origin, hitInfo.point, Color.blue);

                if (hitInfo.collider.gameObject == PlayerManager.Instance.gameObject)
                {
                    if (fadingObject != null)
                    {
                        fadingObject.doFade = false;
                    }
                }
                else
                {
                    fadingObject = hitInfo.collider.gameObject.GetComponent<FadingObject>();
                    if (fadingObject != null)
                    {
                        fadingObject.doFade = true;
                    }
                }
            }
            

        }
    }
}
