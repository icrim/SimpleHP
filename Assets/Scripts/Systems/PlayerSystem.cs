using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine;

public partial class PlayerSystem : SystemBase
{
    private int frameCount = 0;
    private float timeCount = 0;

    protected override void OnUpdate()
    {
        timeCount += Time.DeltaTime * 1000;

        Entities.ForEach((ref Translation translation, ref Player player) =>
        {
            for (int step = frameCount; step < timeCount; ++step)
            {
                if (step % 1000 == 0)// generate new direction
                {
                    float r = UnityEngine.Random.Range(0, 2 * Mathf.PI);
                    player.direction = new Vector2(Mathf.Cos(r), Mathf.Sin(r));
                }

                float3 pos = translation.Value;
                pos.x = Mathf.Min(Mathf.Max((float)-100, pos.x + player.direction.x * (float)0.1), (float)100);
                pos.z = Mathf.Min(Mathf.Max((float)-50, pos.z + player.direction.y * (float)0.1), (float)150);
                if (pos.x == -100 || pos.x == 100 || pos.z == -50 || pos.z == 150)
                    player.direction = -player.direction;
                translation.Value = pos;
                Debug.Log("Wow");
                player.health = Mathf.Min(Mathf.Max((float)0, player.health + (float)0.0005 * (pos.x < 0 ? 1 : -1)), (float)1);
            }
            /*
            setHealth(data.player, data.health);
            */
        }).WithoutBurst().Run();

        while (frameCount < timeCount) ++frameCount;
    }
}
