using UnityEngine;

public class CandidateEnums : MonoBehaviour {
    public enum CandidateType {
        Human,
        NotHuman
    }

    public enum BodyLanguageType {
        Passive,
        Responsive
    }

    [System.Serializable]
    public class QuestionResponse {
        public Question question;

        [TextArea(3, 10)]
        public string responseText;

        public BodyLanguageBehavior responsiveBodyLanguage;
    }

    [System.Serializable]
    public class CandidateStats {
        public string firstName;
        public string lastName;
        public int age;
        public string heightFt;
        public float weightLbs;
    }
}
