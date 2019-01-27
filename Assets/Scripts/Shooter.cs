using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public string projectilePrefabName;
    public Transform SpawnProjectileTransform;
    

    public void ShootTowards(Vector3 direction)
    {
        Projectile projectilePrefab = Resources.Load<Projectile>($"Prefabs/Projectiles/{projectilePrefabName}");
        Projectile projectile = Instantiate(projectilePrefab, SpawnProjectileTransform.position, Quaternion.identity);
        projectile.direction = direction;
    }
}
