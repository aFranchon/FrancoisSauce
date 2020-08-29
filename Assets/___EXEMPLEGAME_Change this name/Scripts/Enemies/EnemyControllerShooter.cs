using System.Collections;
using DG.Tweening;
using FrancoisSauce.Scripts.FSUtils;
using UnityEngine;

public class EnemyControllerShooter : MonoBehaviour
{
    [SerializeField] private Transform playerTransform = null;
    [SerializeField] private GameObjectPool poolShot = null;

    private Transform myTransform;
    [SerializeField] private Collider myCollider = null;

    [SerializeField] private float maxDistanceToShoot = 10f;
    
    private void Awake()
    {
        myTransform = transform;
    }

    [SerializeField] private float bulletTimer = .5f;
    private bool isAbleToShoot = true;
    
    public void OnUpdateGameStarted()
    {
        if (!isAbleToShoot) return;
        var position = myTransform.position;
        var distance = (position - playerTransform.position);

        if (distance.magnitude > maxDistanceToShoot) return;
        
        var newBullet = poolShot.RequestPool();
        
        newBullet.transform.DOMove(distance.normalized * -200, 100f)
            .SetEase(Ease.Linear)
            .From(new Vector3(position.x, 1f, position.z))
            .onComplete += () => newBullet.SetActive(false);

        Physics.IgnoreCollision(myCollider, newBullet.GetComponent<Collider>());
        
        isAbleToShoot = false;
        StartCoroutine(ResetIsAbleToShoot());
    }

    private IEnumerator ResetIsAbleToShoot()
    {
        yield return new WaitForSecondsRealtime(bulletTimer);
        isAbleToShoot = true;
    }
}