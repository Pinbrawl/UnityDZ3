using TMPro;
using UnityEngine;

public class InformationViewer : MonoBehaviour
{
    [SerializeField] private TMP_Text _spawnedText;
    [SerializeField] private string _baseSpawnedMessage;
    [SerializeField] private TMP_Text _instantiateText;
    [SerializeField] private string _baseInstantiateMessage;
    [SerializeField] private TMP_Text _activeText;
    [SerializeField] private string _baseActiveMessage;

    private int _spawnedCount = 0;
    private int _InstantiateCount = 0;

    protected void PrintSpawned()
    {
        _spawnedText.text = _baseSpawnedMessage + ": " + ++_spawnedCount;
    }

    protected void PrintInstantiated()
    {
        _instantiateText.text = _baseInstantiateMessage + ": " + ++_InstantiateCount;
    }

    protected void PrintActive(int count)
    {
        _activeText.text = _baseActiveMessage + ": " + count;
    }
}
