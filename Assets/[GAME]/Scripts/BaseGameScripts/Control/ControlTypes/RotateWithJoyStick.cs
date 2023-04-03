﻿using UnityEngine;

namespace Scripts.BaseGameScripts.Control.ControlTypes
{
    public class RotateWithJoyStick : BaseControl
    {
        [SerializeField]
        private FloatingJoystick floatingJoystick;
        
        [SerializeField]
        private float turnSpeed;
        
        private Vector3 mouseStartPos;
        private Vector3 dir;

        public Vector3 Dir
        {
            get => dir;
            set => dir = value;
        }
        
        protected override void OnTapDown()
        {
            base.OnTapDown();
            mouseStartPos = Input.mousePosition;
        }

        protected override void OnTapHold()
        {
            base.OnTapHold();
            GetInput();
        }

        public override void GetInput()
        {
            dir = -(mouseStartPos - Input.mousePosition).normalized;
            dir = new Vector3(dir.x, 0, dir.y) + TransformOfObj.position;
            
            //TransformOfObj.LookAt(dir, Vector3.up);
            Rotate(dir);
            //LockOnTarget();
        }
        
        private void LockOnTarget ()
        {
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = Quaternion.Lerp(TransformOfObj.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
            TransformOfObj.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        }
        
        void Rotate(Vector3 direction)
        {
            Vector3 desiredForward = Vector3.RotateTowards(TransformOfObj.forward,
                new Vector3(floatingJoystick.Horizontal, 0, floatingJoystick.Vertical), 
                turnSpeed * Time.deltaTime,
                1);
            Quaternion newRotation = Quaternion.LookRotation(desiredForward);
            TransformOfObj.rotation = newRotation;
        }
    }
}