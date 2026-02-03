using UnityEngine;
using DG.Tweening;
namespace SpaceGame
{
    public class EnemyGrenader : Enemy
    {
        [SerializeField] private float coolDown;
        [SerializeField] private int chargeDetection;
        [SerializeField] private float chargeTime;

        [SerializeField] protected GameObject grenadePrefab;

        private SpriteRenderer spriteRenderer;
        private Color regularColor = new Color(255, 231, 0, 255);
        private LineRenderer lineRenderer;
        enum EnemyStates
        {
            Follow,
            Charge,
            Throw,
            Cooldown
        }

        private EnemyStates enemyState = EnemyStates.Follow;

        private float timer;
        protected override void  Start()
        {
            base.Start();
            spriteRenderer = GetComponent<SpriteRenderer>();
            lineRenderer = gameObject.AddComponent<LineRenderer>();
            lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
            lineRenderer.widthMultiplier = 0.1f;
            lineRenderer.positionCount = 2;
            lineRenderer.startColor = regularColor;
            lineRenderer.endColor = regularColor;
            lineRenderer.enabled = false;
        }

        // Update is called once per frame
        protected override void  Update()
        {
            if (target == null)
                return;

            timer += Time.deltaTime;
            switch(enemyState)
            {
                case EnemyStates.Follow:
                    base.Update();
                    if (Vector2.Distance(transform.position, target.position) < chargeDetection)
                    {
                        enemyState = EnemyStates.Charge;
                        // spriteRenderer.DOKill();
                        // spriteRenderer.DOColor(Color.white, chargeTime-0.1f);
                        timer = 0;
                        lineRenderer.enabled = true;  
                    }
                    break;
                case EnemyStates.Charge:
                    Vector2 dir = ((Vector2)target.position - (Vector2)transform.position).normalized;
                    transform.up = dir;
                    lineRenderer.SetPosition(0, transform.position);
                    lineRenderer.SetPosition(1, target.position);
                    if (timer > chargeTime)
                    {
                        enemyState = EnemyStates.Throw;
                        timer = 0;
                        lineRenderer.enabled = false;  
                    }
                    break;
                case EnemyStates.Throw:
                    ThrowGrenade();
                    enemyState = EnemyStates.Cooldown;
                    // spriteRenderer.DOKill();
                    // spriteRenderer.DOColor(regularColor, coolDown-0.1f);
                    timer = 0;
                    break;
                case EnemyStates.Cooldown:
                    if (timer > coolDown)
                    {
                        enemyState = EnemyStates.Follow;
                        timer = 0;
                    }
                    break;
            }
        }

        private void ThrowGrenade()
        {
            Vector3 spawnPos = transform.position + transform.up * 2;
            GameObject grenade = Instantiate(grenadePrefab, spawnPos, Quaternion.identity);
        }
    }
}