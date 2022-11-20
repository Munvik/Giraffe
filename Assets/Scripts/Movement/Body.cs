using Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Movement
{
    public class Body : MonoBehaviour, IBody
    {
        [SerializeField] private bool sendUpdates;

        [Header("Movement")]
        [SerializeField] private float movementRage;

        [Header("Crouching")]
        [SerializeField] private float crouchFrom;
        [SerializeField] private float crouchTo;
        [SerializeField] private List<LegAnimation> legAnims;
        [SerializeField] private AnimationCurve crouchLegsDuringJunp;
        [SerializeField] private AnimationCurve legsSpreadDuringCrouch;

        [Header("Jumping")]
        [SerializeField] private float jumpHeight;
        [SerializeField] private AnimationCurve jumpCurve;
        [SerializeField] private float jumpDuration;
        [SerializeField] private Transform jumpBody;

        [Header("Speeds")]
        [SerializeField] private float movementSpeed;
        [SerializeField] private float crouchSpeed;

        [Range(-1f, 1f)]
        public float targetMovement = 0f;

        [Range(0f, 1f)]
        public float targetCrouch = 0;

        private float jumpStartTime;
        public bool duringJump;

        [SerializeField] private NetworkSender sender;

        void Start()
        {
            if (sendUpdates)
                BeginSend();
        }

        [ContextMenu("Send")]
        public void BeginSend()
        {
            StartCoroutine(SendUpdates());
        }

        private void Update()
        {
            UpdateBodyPosition();
            UpdateCrouchPosition();
            UpdateJump();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump(1f);
            }
        }

        public float sendThreshold;
        private IEnumerator SendUpdates()
        {
            yield return new WaitForSeconds(sendThreshold);
            sender?.Send("OnCrouchUpdate", JsonUtility.ToJson(new FloatData() { floatVal = targetCrouch }));
            yield return new WaitForSeconds(sendThreshold); 
            sender?.Send("OnMoveUpdate", JsonUtility.ToJson(new FloatData() { floatVal = targetMovement }));
            StartCoroutine(SendUpdates());
        }

        public void Crouch(float crouchValue)
        {
            targetCrouch += crouchValue;
            targetCrouch = Mathf.Clamp01(targetCrouch);
        }

        public void Jump(float force)
        {
            if (duringJump)
                return;

            jumpStartTime = Time.time;
            duringJump = true;

            if (sendUpdates)
            {
                sender?.Send("OnJumpUpdate", JsonUtility.ToJson(new FloatData() { floatVal = 1f }));
            }
        }

        public void Move(float movement)
        {
            targetMovement += movement;
            targetMovement = Mathf.Clamp(targetMovement, -1f, 1f);
        }

        private void UpdateBodyPosition()
        {
            Vector3 targetPos = transform.localPosition;
            targetPos.x = Mathf.Lerp(-movementRage, movementRage, Mathf.InverseLerp(-1f, 1f, targetMovement));
            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, Time.deltaTime * movementSpeed);
        }

        private void UpdateCrouchPosition()
        {
            Vector3 targetPos = transform.localPosition;
            targetPos.y = Mathf.Lerp(crouchFrom, crouchTo, targetCrouch);
            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, Time.deltaTime * crouchSpeed);
            foreach (var legAnim in legAnims)
                legAnim.UpdateRot(legsSpreadDuringCrouch.Evaluate(1f - targetCrouch));
        }

        private void UpdateJump()
        {
            if (duringJump)
            {
                float jumpProgress = (Time.time - jumpStartTime) / jumpDuration;                
                float currentHeight = jumpCurve.Evaluate(jumpProgress);
                Vector3 targetPos = jumpBody.transform.localPosition;
                targetPos.y = jumpHeight;

                if (jumpProgress >= 1f)
                {
                    jumpBody.transform.localPosition = Vector3.Lerp(Vector3.zero, targetPos, jumpCurve.Evaluate(1f));
                    duringJump = false;
                    return;
                }

                foreach (var legAnim in legAnims)
                    legAnim.UpdateRot(Mathf.Lerp(legsSpreadDuringCrouch.Evaluate(1f - targetCrouch), 1f, crouchLegsDuringJunp.Evaluate(jumpProgress)));

                jumpBody.transform.localPosition = Vector3.Lerp(Vector3.zero, targetPos, currentHeight);             
            }
        }
    }
}