using UnityEngine;

public abstract class Spawner<T> : MonoBehaviour where T : Component
{
    protected abstract int SpawnedCount { get; }
    protected abstract int CreatedCount { get; }
    protected abstract int ActiveCount { get; }
    protected abstract int StatisticsOrder { get; }
    protected abstract string DisplayName { get; }

    protected virtual void OnGUI()
    {
        Rect area = new Rect(10f, 10f + StatisticsOrder * 82f, 250f, 76f);
        GUI.Box(area, $"{DisplayName}\n" +
                      $"Появилось: {SpawnedCount}\n" +
                      $"Создано: {CreatedCount}\n" +
                      $"Активно: {ActiveCount}");
    }
}
