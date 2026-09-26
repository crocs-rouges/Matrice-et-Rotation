using UnityEngine;
using UnityEngine.InputSystem;

// Author : Romain Chevalier

namespace Com.IsartDigital
{
    public class Shield : MonoBehaviour
    {
        [SerializeField] private GameObject centerPillar;
        [SerializeField] private float distanceToPillar;
        [SerializeField] private float moveSpeed = 3;
        [SerializeField] private Transform shieldPointLeft;
        [SerializeField] private Transform shieldPointRight;
        private float shieldPosY;

        private void Start()
        {
            shieldPosY = transform.position.y;
            Vector3 lDistance = transform.position - centerPillar.transform.position;
            Vector3 lNewPos = lDistance.normalized * distanceToPillar;
            transform.position = new Vector3(lNewPos.x, shieldPosY, lNewPos.z);
        }

        private void Update()
        {
            // Left
            if (Keyboard.current != null && Keyboard.current.aKey.isPressed)
            {
                MakeRotation(shieldPointLeft);
                MakeRotation(shieldPointRight);
            }
            // Right
            if (Keyboard.current != null && Keyboard.current.dKey.isPressed)
            {
                MakeRotation(shieldPointLeft, false);
                MakeRotation(shieldPointRight, false);
            }
            AlignShieldOnPoint();
        }

        /// <summary>
        /// make a rotation of a point around the center pillar
        /// </summary>
        /// <param name="pTransform"></param>
        /// <param name="pGoLeft"></param>
        private void MakeRotation(Transform pTransform, bool pGoLeft = true)
        {
            float lMultiply = pGoLeft ? 1f : -1f;
            float lAngle = lMultiply * (moveSpeed * Time.deltaTime);
            float lCosX = Mathf.Cos(lAngle);
            float lSinY = Mathf.Sin(lAngle);

            Vector4 lMatrice = new Vector4()
            {
                x = lCosX,
                y = -lSinY,
                z = lSinY,
                w = lCosX
            };

            Vector3 lDistance = pTransform.position - centerPillar.transform.position;
            float lPosPillarX = lDistance.x * lMatrice.x;
            lPosPillarX += lDistance.z * lMatrice.y;

            float lPosPillarZ = lDistance.x * lMatrice.z;
            lPosPillarZ += lDistance.z * lMatrice.w;

            Vector3 lNewPos = new Vector3(centerPillar.transform.position.x + lPosPillarX, shieldPosY, lPosPillarZ + centerPillar.transform.position.z);
            pTransform.position = lNewPos;
        }

        /// <summary>
        /// Change shield position and rotation based on shield point left and right
        /// </summary>
        private void AlignShieldOnPoint()
        {
            // Make Shield Position in center of Shield Point
            Vector3 lDistance = shieldPointRight.position - shieldPointLeft.position;
            transform.position = shieldPointLeft.position + (lDistance / 2f);
            transform.position = new Vector3(transform.position.x, shieldPosY, transform.position.z);

            // Make Shield Rotation 
            transform.rotation = Quaternion.LookRotation(lDistance);
        }
    }
}