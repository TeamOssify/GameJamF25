using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Candidate Event Channel", menuName = "Scriptable Objects/Candidate Event Channel")]
public sealed class CandidateEventChannelSO : ScriptableObject {
    public UnityAction<CandidateInstance> OnCandidateEntered;
    public UnityAction<CandidateInstance> OnCandidateExited;
    public UnityAction<CandidateInstance> OnCandidateSatDown;
    public UnityAction<CandidateInstance> OnCandidateStoodUp;

    public void RaiseCandidateEntered(CandidateInstance candidate) {
        Debug.Assert(candidate != null, "candidate != null");
        Debug.Assert(OnCandidateEntered != null, "OnCandidateEntered != null");

        OnCandidateEntered?.Invoke(candidate);
    }

    public void RaiseCandidateExited(CandidateInstance candidate) {
        Debug.Assert(candidate != null, "candidate != null");
        Debug.Assert(OnCandidateExited != null, "OnCandidateExited != null");

        OnCandidateExited?.Invoke(candidate);
    }

    public void RaiseCandidateSatDown(CandidateInstance candidate) {
        Debug.Assert(candidate != null, "candidate != null");
        Debug.Assert(OnCandidateSatDown != null, "OnCandidateSatDown != null");

        OnCandidateSatDown?.Invoke(candidate);
    }

    public void RaiseCandidateStoodUp(CandidateInstance candidate) {
        Debug.Assert(candidate != null, "candidate != null");
        Debug.Assert(OnCandidateStoodUp != null, "OnCandidateStoodUp != null");

        OnCandidateStoodUp?.Invoke(candidate);
    }
}