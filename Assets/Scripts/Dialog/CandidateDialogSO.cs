using Newtonsoft.Json;

using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Candidate Dialog")]
public sealed class CandidateDialogSO : ScriptableObject {
    [SerializeField] private TextAsset dialogJsonFile;

    private DialogFile _dialogFile;

    public DialogFile Dialog {
        get {
            if (_dialogFile == null) {
                _dialogFile = JsonConvert.DeserializeObject<DialogFile>(dialogJsonFile.text);
            }

            return _dialogFile;
        }
    }
}