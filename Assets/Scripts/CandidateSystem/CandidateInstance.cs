using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class CandidateInstance {
    public Candidate CharacterData { get; private set; }
    public CandidateVariant CurrentVariant { get; private set; }

    //randomly gen name
    public string GeneratedFirstName { get; private set; }
    public string GeneratedLastName { get; private set; }
    public int GeneratedAge { get; private set; }
    public float GeneratedWeight { get; private set; }

    public bool? PlayerDecision { get; set; } //null = undecided, true = pass, false = fail
    public string PlayerNotes { get; set; }

    public List<Question> AskedQuestions { get; private set; }
    public bool HasBeenInterviewed { get; set; }

    public CandidateInstance(Candidate characterData, CandidateVariant variant) {
        characterData = characterData;
        CurrentVariant = variant;
        AskedQuestions = new List<Question>();
        PlayerNotes = "";
        HasBeenInterviewed = false;

        GenerateInstanceStats();
    }

    private void GenerateInstanceStats() {
        GeneratedFirstName = "t"; //get the name from sumthin
        GeneratedLastName = "t"; //gen the name

        GeneratedAge = CurrentVariant.baseStats.age + Random.Range(-2, 3);
        GeneratedWeight = CurrentVariant.baseStats.weightLbs + Random.Range(-10, 10);
    }

    // get response //idk how thiss will work yet

    // get body language //james job

    public bool IsHuman() {
        return CurrentVariant.candidateType == CandidateDataStructures.CandidateType.Human;
    }

    public string GetFullName() {
        return $"{GeneratedFirstName} {GeneratedLastName}";
    }
}
