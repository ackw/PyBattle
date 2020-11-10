using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPGM.Gameplay
{
    /// <summary>
    /// A simple camera follower class. It saves the offset from the
    ///  focus position when started, and preserves that offset when following the focus.
    /// </summary>
    public class CameraController : MonoBehaviour
    {
        public Transform focus;
        public float smoothTime = 2;

        Vector3 offset;

        void Awake()
        {
            offset = focus.position - transform.position;
        }

        void Update()
        {
            transform.position = Vector3.Lerp(transform.position, focus.position - offset, Time.deltaTime * smoothTime);
            
            transform.position = new Vector3(
            Mathf.Clamp(focus.position.x, -14.20f, 12.01f),
            Mathf.Clamp(focus.position.y, -9.80f, 15.8f),
            transform.position.z);
        }
    }
}