using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class CandidateCell : MonoBehaviour
{
    [SerializeField] private Image portraitImage;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private Image checkmarkImage;
    [SerializeField] private Image xImage;

    private CandidateInstance _candidateData;

    public void Initialize(CandidateInstance candidate) {
        _candidateData = candidate;
        portraitImage.sprite = candidate.CurrentVariant.portraitSprite;
        nameText.text = candidate.GetFullName();

    }

    public void OnPassToggled() {
        if (checkmarkImage.gameObject.activeSelf) {
            checkmarkImage.gameObject.SetActive(false);

            _candidateData.PlayerDecision = null;
        }
        else {
            checkmarkImage.gameObject.SetActive(true);
            xImage.gameObject.SetActive(false);

            _candidateData.PlayerDecision = true;
        }
        Debug.Log("passed");
    }

    public void OnFailToggled() {
        if (xImage.gameObject.activeSelf) {
            xImage.gameObject.SetActive(false);

            _candidateData.PlayerDecision = null;
        }
        else {
            xImage.gameObject.SetActive(true);
            checkmarkImage.gameObject.SetActive(false);

            _candidateData.PlayerDecision = false;
        }
        Debug.Log("failed");
    }

    public void OnCellClicked() {
        //open notes
        Debug.Log("opening notes");
    }

    public CandidateInstance Get_candidateData() {
        return _candidateData;
    }
}
