using System.Collections;

using UnityEngine;
using System.Collections.Generic;
using System.Threading;

public class CandidatePhysicalManager : MonoBehaviour {
    [SerializeField]
    private SpriteRenderer candidateSpriteRenderer;
    [SerializeField]
    private Transform candidateTransform;
    [SerializeField]
    private CandidateManager candidateManager;

    [SerializeField]
    private float speed;
    [SerializeField]
    private AnimationCurve walkCurve = AnimationCurve.Linear(0,0,1,1);
    [SerializeField]
    private float pauseBetweenMovement;

    [SerializeField]
    private Transform doorTarget;
    [SerializeField]
    private Transform initialWalkTarget;
    [SerializeField]
    private Transform chairTarget;
    [SerializeField]
    private Transform sitTarget;

    private bool isWalking = false;
    void Awake() {
        if (candidateSpriteRenderer != null) {
            candidateSpriteRenderer.enabled = false;
        }
        //candidateSpriteRenderer.enabled = true;
        //WalkToChair();
        //WalkToDoor();
    }

    public void SpawnCandidateImage() {
        var candidate = candidateManager.GetCurrentCandidate();

        candidateSpriteRenderer.sprite = candidate.CurrentVariant.fullBodySprite;
        candidateSpriteRenderer.enabled = true;

        candidateTransform.position = doorTarget.position;
    }

    public void WalkToChair() {
        if (!isWalking) {
            StartCoroutine(WalkToChairSequence());
        }
    }

    public void WalkToDoor() {
        if (!isWalking) {
            StartCoroutine(WalkToDoorSequence());
        }
    }

    private IEnumerator WalkToChairSequence() {
        yield return StartCoroutine(Walk(initialWalkTarget.position));
        yield return new WaitForSeconds(pauseBetweenMovement);
        yield return StartCoroutine(Walk(chairTarget.position));
        yield return new WaitForSeconds(pauseBetweenMovement);
        yield return StartCoroutine(Walk(sitTarget.position));
    }

    private IEnumerator WalkToDoorSequence() {
        yield return StartCoroutine(Walk(chairTarget.position));
        yield return new WaitForSeconds(pauseBetweenMovement);
        yield return StartCoroutine(Walk(initialWalkTarget.position));
        yield return new WaitForSeconds(pauseBetweenMovement);
        yield return StartCoroutine(Walk(doorTarget.position));
    }
    private IEnumerator Walk(Vector3 target) {
        isWalking = true;
        Vector3 startPos = candidateTransform.position;
        float distance = Vector3.Distance(startPos, target);
        float duration = distance / speed;
        float elapsed = 0f;

        while (elapsed < duration) {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            float curvedT = walkCurve.Evaluate(t);

            candidateTransform.position = Vector3.Lerp(startPos, target, curvedT);

            yield return null;
        }

        candidateTransform.position = target;
        isWalking = false;
    }
}
