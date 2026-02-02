using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

namespace SpaceGame
{
    public class CinemachineManager : MonoBehaviour
    {
        [SerializeField] private CinemachineCamera cCam;
        public void SetTarget()
        {
            StartCoroutine(SetTargetDelay());
        }
        IEnumerator SetTargetDelay()
        {
            yield return new WaitForSeconds(0.1f);
            cCam.Follow = GameManager.GetInstance().GetPlayer().transform;
        }
    }
}