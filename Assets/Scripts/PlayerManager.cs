using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int PlayerCount = 100;
    public GameObject PlayerPrefab;

    private EntityManager _entityManager;
    private BlobAssetStore _store;
    private GameObjectConversionSettings _settings;

    // Start is called before the first frame update
    void Start()
    {
        _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        _store = new BlobAssetStore();
        _settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, _store);
        Entity entityPrefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(PlayerPrefab, _settings);

        for (int i = 0; i < PlayerCount; ++i)
        {
            Entity entity =_entityManager.Instantiate(entityPrefab);
            Translation translation = new Translation
            {
                Value = new float3
                {
                    x = UnityEngine.Random.Range((float)-100, 100),
                    y = 0,
                    z = 50 + UnityEngine.Random.Range((float)-50, 150)
                }
            };
            _entityManager.SetComponentData(entity, translation);
            Debug.Log("hello");
            /*

            Vector3 pos = newPlayer.transform.position;
            pos.x = Random.Range((float)-100, 100);
            pos.z = Random.Range((float)-50, 150);
            newPlayer.transform.position = pos;

            setHealth(newPlayer, Random.Range((float)0, 1));

            PlayerData playerData = new PlayerData(newPlayer);
            players.Add(playerData);*/
        }
    }

    private void OnDestroy()
    {
        _store.Dispose();
    }
}
