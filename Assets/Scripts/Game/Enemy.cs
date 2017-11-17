using UnityEngine;
using Random = System.Random;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : PoolableEntity
{
    private readonly Random _random = new Random();
    [SerializeField] private DropTable _dropTable;
    [SerializeField] private GameObject _explosion;
    [SerializeField] private float _health = 1f;

    private float Health
    {
        get { return _health; }
        set
        {
            _health = value;
            if (!(_health <= 0f)) return;
            Die();
        }
    }

    protected override void OnDie()
    {
        if (_explosion) Instantiate(_explosion, transform.position, Quaternion.identity);
        FindObjectOfType<Game>().Score += 10;
        var rng = (float) _random.NextDouble();
        foreach (var dropItem in _dropTable.DropItems)
            if (rng * 100 <= dropItem.DropChance)
            {
                var drop = ObjectPool.Instance.GetPooledObject(dropItem.Drop.GetType());
                drop.transform.position = transform.position;
                break;
            }
    }

    protected override void OnCollide(Collider2D col)
    {
        base.OnCollide(col);
        Bullet bullet;
        if ((bullet = col.gameObject.GetComponent<Bullet>()) != null)
            Health -= bullet.Power;
    }

    public override void ResetToDefault(PoolableEntity origin)
    {
        var castedOrigin = (Enemy) origin;
        _health = castedOrigin._health;
    }
}