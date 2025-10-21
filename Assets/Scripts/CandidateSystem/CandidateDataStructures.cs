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
    
}
