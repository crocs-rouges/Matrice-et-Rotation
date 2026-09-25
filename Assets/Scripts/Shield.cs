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
                RotationLeft();
        }

        private void MoveLeft()
        {
            // Use cosinus and sinus to move with radiants
            Vector3 lDistance = transform.position - centerPillar.transform.position;
            Vector3 lDirection = lDistance.normalized;

            // make a rotation on Direction
            float lCosX = Mathf.Cos(lDirection.x);
            float lSinY = Mathf.Sin(lDirection.z);
            Vector4 lMatrice = new Vector4()
            {
                x = lCosX,
                y = -lSinY,
                z = lSinY,
                w = lCosX
            };
            float lPosX = transform.position.x * lMatrice.x;
            lPosX += transform.position.z * lMatrice.y;

            float lPosZ = transform.position.x * lMatrice.z;
            lPosZ += transform.position.z * lMatrice.w;

            Vector3 lNewPos = new Vector3(lPosX, shieldPosY, lPosZ);

            transform.position = lNewPos;
        }

        private void MoveRight()
        {
            // Use cosinus and sinus to move with radiants
            Vector3 lDistance = transform.position - centerPillar.transform.position;
            Vector3 lDirection = lDistance.normalized;

            // make a rotation on Direction
            float lCosX = Mathf.Cos(lDirection.x);
            lCosX = lDirection.x;
            float lSinY = Mathf.Sin(lDirection.z);
            lSinY = lDirection.z;
            Vector4 lMatrice = new Vector4()
            {
                x = lCosX,
                y = -lSinY,
                z = lSinY,
                w = lCosX
            };
            float lPosX = transform.position.x * lMatrice.x;
            lPosX -= transform.position.z * lMatrice.y;

            float lPosZ = transform.position.x * lMatrice.z;
            lPosZ -= transform.position.z * lMatrice.w;

            Vector3 lNewPos = new Vector3(lPosX * distanceToPillar, shieldPosY, lPosZ * distanceToPillar);

            transform.position = lNewPos;
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


            float lPosX = transform.position.x * lMatrice.x;
            lPosX += transform.position.z * lMatrice.y;

            float lPosZ = transform.position.x * lMatrice.z;
            lPosZ += transform.position.z * lMatrice.w;


            Vector3 lNewPos = new Vector3(lPosPillarX + lPosX, shieldPosY, lPosPillarZ + lPosZ);
            transform.position = lNewPos;
        }
    }
}