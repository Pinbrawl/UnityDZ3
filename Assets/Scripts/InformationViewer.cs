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

    protected void PrintSpawned(int count)
    {
        _spawnedText.text = _baseSpawnedMessage + ": " + count;
    }

    protected void PrintInstantiated(int count)
    {
        _instantiateText.text = _baseInstantiateMessage + ": " + count;
    }

    protected void PrintActive(int count)
    {
        _activeText.text = _baseActiveMessage + ": " + count;
    }
}
