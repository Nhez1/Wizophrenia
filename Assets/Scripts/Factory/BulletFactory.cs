using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Factory 
{
public class BulletFactory : Factory
{

    [SerializeField] Bullet _bulletProduct;

    public override IProduct GetProduct(Vector3 position)
    {
        var createBullet = Instantiate(_bulletProduct, position, Quaternion.identity);
        createBullet.Initialize();

        return createBullet;
    }

}

}


