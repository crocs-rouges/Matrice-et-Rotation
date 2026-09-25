using UnityEngine;

// Author : Romain Chevalier

namespace Com.Skate.Manager
{
    public class CameraManager : MonoBehaviour
    {
        private static CameraManager instance;
        [SerializeField] private GameObject target;

        [Header("Dynamic Zoom & Look-ahead")]
        [SerializeField] private float minOrthographicSize = 5f;
        [SerializeField] private float maxOrthographicSize = 15f;
        [SerializeField] private float zoomSpeed = 3f;
        [SerializeField] private float positionLerpSpeed = 5f;
        [SerializeField] private float paddingBottom = 2f;
        [SerializeField] private float maxRaycastDistance = 50f;
        [SerializeField] private LayerMask groundLayerMask; // Fallback to Ground and Rail
        private Vector2 basePositionOffset = new Vector2(0, 25);
        private Camera cam;
        private const float Z_OFFSET = -10f;

        // Level Design driven Parameters
        private float targetOverrideZoom = 0f;
        private Vector2 overrideOffset = Vector2.zero;

        // Actual Offset in Lerp
        [SerializeField] private Vector2 currentOffset = new Vector2(2, 4);

        public static CameraManager GetInstance()
        {
            if (instance == null) instance = FindObjectOfType<CameraManager>();
            return instance;
        }

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }
            instance = this;
            cam = GetComponent<Camera>();

            if (groundLayerMask.value == 0)
                groundLayerMask = LayerMask.GetMask("Ground", "Rail");

            basePositionOffset = currentOffset;
        }

        private void LateUpdate()
        {
            if (target == null) return;
            HandleCameraTransform();
            HandleDynamicZoom();
        }

        private void HandleCameraTransform()
        {
            // We only lerp the offset, not the absolute position
            // to ensure no motion blur occur
            currentOffset = Vector2.Lerp(currentOffset, overrideOffset, Time.deltaTime * positionLerpSpeed);

            // Camera follow Target + lerp Offset
            Vector3 lTargetPos = target.transform.position + (Vector3)currentOffset;
            lTargetPos.z = Z_OFFSET;

            transform.position = lTargetPos;
        }

        private void HandleDynamicZoom()
        {
            if (cam == null || !cam.orthographic) return;

            // Base Zoom (Default of forced by the LD)
            float lTargetSize = (targetOverrideZoom > 0f) ? targetOverrideZoom : minOrthographicSize;

            // dynamique Security : Downwards Raycast from the player to ensure we always see the ground
            RaycastHit2D lHit = Physics2D.Raycast(target.transform.position, Vector2.down, maxRaycastDistance, groundLayerMask);

            if (lHit.collider != null)
            {
                float lDistanceToGround = target.transform.position.y - lHit.point.y;

                // If distance to ground require more Zoom
                float lRequiredSizeForGround = lDistanceToGround + paddingBottom;
                if (lRequiredSizeForGround > lTargetSize)
                    lTargetSize = lRequiredSizeForGround;
            }
            else
                // No Ground Detect = Free Fall so Max Zoom applied
                lTargetSize = maxOrthographicSize;

            lTargetSize = Mathf.Clamp(lTargetSize, minOrthographicSize, maxOrthographicSize);

            // Lerp for smooth Zoom
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, lTargetSize, Time.deltaTime * zoomSpeed);
        }

        public void SetOverride(float pTargetZoom, Vector2 pOffset)
        {
            targetOverrideZoom = pTargetZoom;
            overrideOffset = pOffset;
        }

        public void ClearOverride()
        {
            targetOverrideZoom = 0f;
            overrideOffset = basePositionOffset;
        }
    }
}