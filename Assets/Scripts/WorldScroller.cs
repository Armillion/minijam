using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldScroller : MonoBehaviour {
    public static WorldScroller Instance { get; private set; }

    [SerializeField]
    Transform platformPrefab;

    [SerializeField]
    Vector2 bounds = new(10f, 10f);

    [SerializeField]
    FloatRange platformWidthRange;

    [SerializeField]
    float platformSpawnInterval = 1f;

    List<Transform> platforms = new();

    float platformSpawnTimer = 0f;

    float platformsMargin = 0.5f;

    void Awake() {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }

    void Update() {
        platformSpawnTimer += Time.deltaTime * platformSpawnInterval * GameManager.GameSpeed;

        while (platformSpawnTimer >= 1f) {
            platformSpawnTimer -= 1f;
            int platfomsToSpawn = Random.Range(1, 4);
            List<FloatRange> availableSpace = new() { new FloatRange(-bounds.x, bounds.x) };

            for (int i = 0; i < platfomsToSpawn; i++) {
                var platform = Instantiate(platformPrefab, transform);
                FloatRange randomRange = availableSpace[Random.Range(0, availableSpace.Count)];
                float platformWidth = platformWidthRange.RandomValueInRange;

                platform.localScale = new Vector3(
                    platformWidth - platformsMargin * 2f,
                    platform.localScale.y,
                    platform.localScale.z
                );

                randomRange.min += platformWidth * 0.5f;
                randomRange.max -= platformWidth * 0.5f;

                platform.localPosition = new Vector3(
                    randomRange.RandomValueInRange,
                    bounds.y,
                    platform.position.z
                );

                platforms.Add(platform);

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

                if (availableSpace.Count == 0)
                    break;
            }
        }

        List<Transform> platformsToRemove = new();

        foreach (var platform in platforms) {
            platform.position += GameManager.GameSpeed * Time.deltaTime * Vector3.down;

            if (platform.position.y < -bounds.y - platform.localScale.y * 0.5f)
                platformsToRemove.Add(platform);
        }

        foreach (var platform in platformsToRemove) {
            platforms.Remove(platform);
            Destroy(platform.gameObject);
        }
    }
}
