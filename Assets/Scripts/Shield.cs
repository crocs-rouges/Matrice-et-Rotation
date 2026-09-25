using UnityEngine;
using System;
using UnityEngine.InputSystem;
using Unity.Mathematics;

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
                RotationLeft();
            // Right
            if (Keyboard.current != null && Keyboard.current.dKey.isPressed)
                RotationRight();
        }

        private void RotationLeft()
        {
            float lAngle = moveSpeed * Time.deltaTime;
            float lCosX = Mathf.Cos(lAngle);
            float lSinY = Mathf.Sin(lAngle);

            Vector4 lMatrice = new Vector4()
            {
                x = lCosX,
                y = -lSinY,
                z = lSinY,
                w = lCosX
            };

            Vector3 lDistance = transform.position - centerPillar.transform.position;
            float lPosPillarX = lDistance.x * lMatrice.x;
            lPosPillarX += lDistance.z * lMatrice.y;

            float lPosPillarZ = lDistance.x * lMatrice.z;
            lPosPillarZ += lDistance.z * lMatrice.w;

            Vector3 lNewPos = new Vector3(centerPillar.transform.position.x + lPosPillarX, shieldPosY, lPosPillarZ + centerPillar.transform.position.z);
            transform.position = lNewPos;
        }

        private void RotationRight()
        {
            float lAngle = -(moveSpeed * Time.deltaTime);
            float lCosX = Mathf.Cos(lAngle);
            float lSinY = Mathf.Sin(lAngle);

            Vector4 lMatrice = new Vector4()
            {
                x = lCosX,
                y = -lSinY,
                z = lSinY,
                w = lCosX
            };

            Vector3 lDistance = transform.position - centerPillar.transform.position;
            float lPosPillarX = lDistance.x * lMatrice.x;
            lPosPillarX += lDistance.z * lMatrice.y;

            float lPosPillarZ = lDistance.x * lMatrice.z;
            lPosPillarZ += lDistance.z * lMatrice.w;

            Vector3 lNewPos = new Vector3(centerPillar.transform.position.x + lPosPillarX, shieldPosY, lPosPillarZ + centerPillar.transform.position.z);
            transform.position = lNewPos;
        }
    }
}