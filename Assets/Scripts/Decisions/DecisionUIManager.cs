using TMPro;

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
public class DecisionUIManager : MonoBehaviour {
    [SerializeField] private GameObject decisionUI;
    [SerializeField] private GameObject candidateCell;
    [SerializeField] private GameObject cellContainer;
    [SerializeField] private TMP_InputField notesText;

    private List<CandidateCell> _activeCells = new List<CandidateCell>();

    public void AddCandidate(CandidateInstance candidate) {
        GameObject cellObj = Instantiate(candidateCell, cellContainer.transform);
        CandidateCell cell = cellObj.GetComponent<CandidateCell>();

        if (cell != null) {
            cell.Initialize(candidate);
            cell.updateNotes.AddListener(() => UpdateNotes(candidate));
            _activeCells.Add(cell);
        }
    }

    public void ClearCells() {
        foreach (CandidateCell cell in _activeCells) {
            if (cell != null) {
                Destroy(cell.gameObject);
            }
        }
        _activeCells.Clear();
    }

    private void UpdateNotes(CandidateInstance candidate) {
        notesText.text = candidate.PlayerNotes;
    }

}
