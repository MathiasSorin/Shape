//Made by: Mathias Sorin
//Last updated: 18/06/2021

using MLAPI;
using MLAPI.Messaging;

public class EnemyTableHead : EnemyMain
{
    //Components
    private EnemyAI enemyAI;

    private void Start()
    {
        enemyAI = GetComponent<EnemyAI>();
        InitializeHealthUI();
    }

    private void Update()
    {
        UpdateLocalVariables();
        CalculateHealthUI();
        enemyAI.SimpleEnemyBehaviorServerRpc();
    }

    public override void GetAggroed(float aggro, PlayerClass agressor)
    {
        base.GetAggroed(aggro, agressor);
    }

    public override void GetDamaged(float damage)
    {
        base.GetDamaged(damage);
    }

    public override void GetHealed(float heal)
    {
        //Do stuff
    }

    public override void GetTargeted(bool on)
    {
        base.GetTargeted(on);
    }

    //OwnerShip = true so that only host can call this function (function is currently getting called from animation event)
    [ServerRpc(RequireOwnership = true)]
    public void Attack1ServerRpc()
    {
        GameManager.Instance.abilityCatalog.TableHeadAbility1(transform);
    }
}
