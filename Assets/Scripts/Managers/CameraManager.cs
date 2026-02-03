using UnityEngine;
using DG.Tweening;
using System;
using Unity.IO.LowLevel.Unsafe;



#if UNITY_EDITOR
using UnityEditor;
#endif

namespace SpaceGame
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private float shakeDuration;
        [SerializeField] private float shakeStrength;
        [SerializeField] private int shakeVibrato;

        private int cameraShakePriority = 0;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void ShakeCamera(float duration = -1f, float strength = -1f, int vibrato = -1, int priority = 1)
        {
            if (duration == -1f)
                duration = shakeDuration;
            if (strength == -1f)
                strength = shakeStrength;
            if (vibrato == -1f)
                vibrato = shakeVibrato;
            
            if (priority < cameraShakePriority)
                return;

            cameraShakePriority = priority;
            transform.DOKill();
            transform.DOShakePosition(duration, strength, vibrato, 1, false).OnComplete(() => {
                cameraShakePriority = 0;
            });
        }


        #if UNITY_EDITOR
            // This will create a button in the Inspector

            // Optional: Draw a proper button in Inspector using this trick
            [CustomEditor(typeof(CameraManager))]
            public class CameraEditor : Editor
            {
                public override void OnInspectorGUI()
                {
                    base.OnInspectorGUI();

                    if (GUILayout.Button("Bump Camera"))
                    {
                        (target as CameraManager).ShakeCamera();
                    }
                }
            }
        #endif

    }
}