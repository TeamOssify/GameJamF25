using System.Collections;

using UnityEngine;

using UnityEngine.Serialization;

public class CandidatePhysicalManager : MonoBehaviour {
    [SerializeField] private SpriteRenderer candidateSpriteRenderer;
    [SerializeField] private Transform candidateTransform;

    [FormerlySerializedAs("speed")] [SerializeField] private float walkSpeed;
    [SerializeField] private AnimationCurve walkCurve = AnimationCurve.Linear(0,0,1,1);
    [SerializeField] private float pauseBetweenMovement;

    [SerializeField] private Transform doorTarget;
    [SerializeField] private Transform initialWalkTarget;
    [SerializeField] private Transform chairTarget;
    [SerializeField] private Transform sitTarget;

    private bool _isWalking = false;
    public bool IsCandidatePresent { get; private set; }

    void Awake() {
        if (candidateSpriteRenderer) {
            candidateSpriteRenderer.enabled = false;
        }
    }

    public void SpawnCandidateImage(CandidateInstance currentCandidate) {
        IsCandidatePresent = true;
        candidateSpriteRenderer.sprite = currentCandidate.CurrentVariant.fullBodySprite;
        candidateSpriteRenderer.enabled = true;

        candidateTransform.position = doorTarget.position;
    }

    public void WalkToChair() {
        if (!IsCandidatePresent) {
            return;
        }

        if (!_isWalking) {
            StartCoroutine(WalkToChairSequence());
        }
    }

    public void WalkToDoor() {
        if (!IsCandidatePresent) {
            return;
        }

        if (!_isWalking) {
            StartCoroutine(WalkToDoorSequence());
        }
    }

    private IEnumerator WalkToChairSequence() {
        yield return Walk(initialWalkTarget.position);
        yield return WaitForSecondsCache.Get(pauseBetweenMovement);
        yield return Walk(chairTarget.position);
        yield return WaitForSecondsCache.Get(pauseBetweenMovement);
        yield return Walk(sitTarget.position);
    }

    private IEnumerator WalkToDoorSequence() {
        yield return Walk(chairTarget.position);
        yield return WaitForSecondsCache.Get(pauseBetweenMovement);
        yield return Walk(initialWalkTarget.position);
        yield return WaitForSecondsCache.Get(pauseBetweenMovement);
        yield return Walk(doorTarget.position);

        candidateSpriteRenderer.enabled = false;
        IsCandidatePresent = false;
    }

    private IEnumerator Walk(Vector3 target) {
        _isWalking = true;

        var startPos = candidateTransform.position;
        var distance = Vector3.Distance(startPos, target);
        var duration = distance / walkSpeed;
        var elapsed = 0f;

        while (elapsed < duration) {
            elapsed += Time.deltaTime;
            var t = elapsed / duration;
            var curvedT = walkCurve.Evaluate(t);

            candidateTransform.position = Vector3.Lerp(startPos, target, curvedT);

            yield return null;
        }

        candidateTransform.position = target;
        _isWalking = false;
    }
}
