using System.Collections.Generic;
using System.ComponentModel;

using JetBrains.Annotations;

public sealed class DialogFile {
    [Description("An entry of dialog from either the player or the candidate.")]
    public sealed class DialogEntry {
        [Description("Label used for dialog flow control.")]
        [CanBeNull]
        public string Label { get; set; }

        [Description("The dialog that the candidate will say. Mutually exclusive with Player.")]
        [CanBeNull]
        public string Candidate { get; set; }

        [Description("Candidate body language to be displayed while the text block for this entry appears")]
        public BodyLanguageType BodyLanguage { get; set; } = BodyLanguageType.None;

        [Description("The dialog that the player will say. Mutually exclusive with Candidate.")]
        [CanBeNull]
        public string Player { get; set; }

        [Description("The responses the player can choose from, paired with the label of the entry to jump to.")]
        [CanBeNull] public Dictionary<string, string> PlayerResponses { get; set; }

        [Description("Stop advancing the dialog tree after this entry.")]
        public bool Stop { get; set; } = false;
    }

    [Description("A sequence of dialog entries.")]
    public sealed class DialogTree {
        [Description("The tree of dialog entries to be played for this type of dialog.")]
        public DialogEntry[] Tree { get; set; }
    }

    [Description("The set of all possible questions and dialog sequences for a given candidate.")]
    public sealed class DialogSet {
        [Description("Intro dialog.")]
        public DialogTree[] Intro { get; set; }

        public DialogTree[] YouAndYourChildAreStarving { get; set; }

        public DialogTree[] YouSeeAStuckTurtle { get; set; }

        public DialogTree[] YouSeeADeer { get; set; }

        public DialogTree[] WhoWouldYouBringForDinner { get; set; }
    }

    [Description("Human dialog.")]
    public DialogSet Human { get; set; }

    [Description("Non-human dialog.")]
    public DialogSet NonHuman { get; set; }
}