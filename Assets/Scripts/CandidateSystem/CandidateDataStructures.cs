using System;

using UnityEngine;

public class CandidateDataStructures : MonoBehaviour {
    public enum CandidateType {
        Human,
        NotHuman
    }

    public enum BodyLanguageType {
        Passive,
        Responsive
    }

    [Serializable]
    public class CandidateStats {
        public int age;
        public string heightFt;
        public float weightLbs;
    }
}
