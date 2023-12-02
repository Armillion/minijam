using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WorldScroller : MonoBehaviour {
    public static WorldScroller Instance { get; private set; }

    [SerializeField]
    Transform platformPrefab;

    [SerializeField]
    Transform enemyPrefab;

    [SerializeField, Space]
    Vector2 bounds = new(10f, 10f);

    [SerializeField, Space]
    FloatRange platformWidthRange;

    [SerializeField]
    float platformSpawnInterval = 1f;

    [SerializeField, Range(0f, 1f)]
    float platformEnemySpawnChance = 0.2f;

    [SerializeField]
    List<Transform> platforms = new();

    List<Transform> prevPlatforms = new();

    float platformSpawnTimer = 1f;

    float platformsMargin = 0.5f;

    void Awake() {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }

    void Update() {
        platformSpawnTimer += Time.deltaTime * platformSpawnInterval;

        if (platformSpawnTimer >= 1f) {
            platformSpawnTimer = 0f;
            int platfomsToSpawn = Random.Range(1, 4);
            List<FloatRange> availableSpace = new() { new FloatRange(-bounds.x, bounds.x) };

            for (int i = 0; i < platfomsToSpawn; i++) {
                var platform = SpawnPlatform(availableSpace, out float platformWidth);
                availableSpace = PartitionAvailableSpace(availableSpace, platform, platformWidth);

                if (Random.value < Mathf.Clamp(platformEnemySpawnChance * Time.timeScale, 0f, 0.8f)) {
                    var enemy = Instantiate(enemyPrefab, platform.position + 0.5f * platform.localScale.y * Vector3.up, Quaternion.identity, transform);
                    enemy.GetComponent<Enemy>().platformWidth = platform.GetComponent<BoxCollider2D>().bounds.size.x * 8f;
                }

                if (availableSpace.Count == 0)
                    break;
            }
        }

        List<Transform> platformsToRemove = new();

        foreach (var platform in platforms) {
            platform.position += Time.deltaTime * Vector3.down;

            if (platform.position.y < -bounds.y - platform.localScale.y * 0.5f)
                platformsToRemove.Add(platform);
        }

        foreach (var platform in platformsToRemove) {
            platforms.Remove(platform);
            Destroy(platform.gameObject);
        }
    }

    List<FloatRange> PartitionAvailableSpace(List<FloatRange> availableSpace, Transform platform, float platformWidth) {
        availableSpace = availableSpace
            .SelectMany(space => {
                float spaceLeft = space.min - platform.localPosition.x - platformWidth * 0.5f - platformsMargin;
                float spaceRight = space.max - platform.localPosition.x + platformWidth * 0.5f + platformsMargin;

                if (spaceLeft > 0f && spaceRight > 0f)
                    return new[] {
                        new FloatRange(space.min, platform.localPosition.x - platformWidth * 0.5f - platformsMargin),
                        new FloatRange(platform.localPosition.x + platformWidth * 0.5f + platformsMargin, space.max)
                    };
                else if (spaceLeft > 0f)
                    return new[] {
                        new FloatRange(space.min, platform.localPosition.x - platformWidth * 0.5f - platformsMargin)
                    };
                else if (spaceRight > 0f)
                    return new[] {
                        new FloatRange(platform.localPosition.x + platformWidth * 0.5f + platformsMargin, space.max)
                    };
                else
                    return new FloatRange[0];
            })
            .Where(space => space.max - space.min > platformWidth + platformsMargin * 2f)
            .ToList();

        return availableSpace;
    }

    Transform SpawnPlatform(List<FloatRange> availableSpace, out float platformWidth, bool spawnSafe = false) {
        var platformTransform = Instantiate(platformPrefab, transform);
        FloatRange randomRange = availableSpace[Random.Range(0, availableSpace.Count)];
        platformWidth = platformWidthRange.RandomValueInRange;

        Platform platform = platformTransform.GetComponent<Platform>();
        platform.Size = new Vector2(platformWidth - platformsMargin * 2f, platform.Size.y);

        randomRange.min += platformWidth * 0.5f;
        randomRange.max -= platformWidth * 0.5f;

        platformTransform.localPosition = new Vector3(
            randomRange.RandomValueInRange,
            bounds.y,
            platformTransform.position.z
        );

        if (spawnSafe) {
            while (!prevPlatforms.Any(p => Vector3.Distance(p.position, platformTransform.position) < 6f)) {
                platformTransform.localPosition = new Vector3(
                    randomRange.RandomValueInRange,
                    bounds.y,
                    platformTransform.position.z
                );
            }
        }

        platforms.Add(platformTransform);
        return platformTransform;
    }
}
