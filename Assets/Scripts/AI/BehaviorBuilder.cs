using UnityEngine;

public class BehaviorBuilder
{
    public static BehaviorTree MakeTree(EnemyController agent)
    {
        BehaviorTree result = null;
        if (agent.monster == "warlock")
        {
            result = new Sequence(new BehaviorTree[] {
                                        new GoTo(AIWaypointManager.Instance.Get(4).transform, 4f),
                                        new PermaBuff(),
                                        new Heal()
                
                /*new MoveToPlayer(agent.GetAction("attack").range),
                                        new Attack(),
                                        new PermaBuff(),
                                        new Heal(),
                                        new Buff()*/
                                     });
        }
        else if (agent.monster == "zombie") {
            result = new Selector(new BehaviorTree[] {
                new Sequence(new BehaviorTree[] {
                    new NearbyEnemiesQuery(5, 7f),
                    new MoveToPlayer(4f),
                    new Attack()
                }),
                new GoTo(AIWaypointManager.Instance.Get(4).transform, 4f)
            });
            /*result = new Sequence(new BehaviorTree[] {
                                       new MoveToPlayer(agent.GetAction("attack").range),
                                       new Attack()
                                     });*/
        }
        else
        {
            result = new Sequence(new BehaviorTree[] {
                                       new MoveToPlayer(agent.GetAction("attack").range),
                                       new Attack()
                                     });
        }

        // do not change/remove: each node should be given a reference to the agent
        foreach (var n in result.AllNodes())
        {
            n.SetAgent(agent);
        }
        return result;
    }
}
