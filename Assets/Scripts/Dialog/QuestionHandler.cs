using UnityEngine;

public sealed class QuestionHandler : MonoBehaviour {
    [SerializeField] private DialogEventChannelSO dialogEventChannel;
    [SerializeField] private DialogTreeType dialogTreeType;
    [SerializeField] private GameObject questionUi;

    public void OnClick() {
        questionUi.gameObject.SetActive(false);
        dialogEventChannel.RaiseAskStandardQuestion(dialogTreeType);
    }
}