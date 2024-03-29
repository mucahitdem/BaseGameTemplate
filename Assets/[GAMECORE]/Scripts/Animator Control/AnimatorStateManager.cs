using Scripts.BaseGameScripts.ComponentManagement;
using Sirenix.Utilities;
using UnityEngine;

namespace Scripts.BaseGameScripts.Animator_Control
{
    public class AnimatorStateManager : BaseComponent
    {
        private string _prevBoolKey;
        private string _prevTriggerKey;

        [SerializeField]
        private AnimatorParameterController animatorParameterController;

        [SerializeField]
        private Animator animOfObj;

        public override Animator AnimOfObj => animOfObj;

        private void Awake()
        {
            animatorParameterController.CalculateKeys();
        }


        public bool GetBool(string key)
        {
            return AnimOfObj.GetBool(animatorParameterController.GetHashKey(key));
        }

        public void SetBool(string key, bool isEnabled, bool reset = false)
        {
            if (reset)
                ResetAllPrevKey();

            AnimOfObj.SetBool(animatorParameterController.GetHashKey(key), isEnabled);
            _prevBoolKey = key;
        }

        public void SetFloat(string key, float value)
        {
            AnimOfObj.SetFloat(animatorParameterController.GetHashKey(key), value);
        }

        public void SetInt(string key, int value)
        {
            AnimOfObj.SetInteger(animatorParameterController.GetHashKey(key), value);
        }

        public void SetTrigger(string key, bool isEnabled, bool reset = false)
        {
            if (reset)
                ResetAllPrevKey();

            if(isEnabled)
                AnimOfObj.SetTrigger(animatorParameterController.GetHashKey(key));
            else
                AnimOfObj.ResetTrigger(animatorParameterController.GetHashKey(key));

            _prevTriggerKey = key;
        }


        private void ResetAllPrevKey()
        {
            StopPlaying();
            if (!_prevBoolKey.IsNullOrWhitespace())
                AnimOfObj.SetBool(animatorParameterController.GetHashKey(_prevBoolKey), false);

            if (!_prevTriggerKey.IsNullOrWhitespace())
                AnimOfObj.ResetTrigger(animatorParameterController.GetHashKey(_prevTriggerKey));
        }

        private void StopPlaying()
        {
            animOfObj.Rebind();
        }
    }
}