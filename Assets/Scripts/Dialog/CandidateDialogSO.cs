using UnityEngine;

public sealed class CandidateDialogSO : ScriptableObject {
    [SerializeField] private TextAsset dialogJsonFile;

    private DialogFile _dialogFile;

    public DialogFile Dialog => _dialogFile ??= JsonUtility.FromJson<DialogFile>(dialogJsonFile.text);
}