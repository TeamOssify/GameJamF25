using UnityEngine;

public static class DialogTreeChooser {
    public static DialogFile.DialogTree GetTree(DialogFile.DialogSet dialogSet, DialogTreeType treeType) {
        DialogFile.DialogTree[] trees;

        switch (treeType) {
            case DialogTreeType.Intro:
                trees = dialogSet.Intro;
                break;
            case DialogTreeType.YouAndYourChildAreStarving:
                trees = dialogSet.YouAndYourChildAreStarving;
                break;
            case DialogTreeType.YouSeeAStuckTurtle:
                trees = dialogSet.YouSeeAStuckTurtle;
                break;
            case DialogTreeType.YouSeeADeer:
                trees = dialogSet.YouSeeADeer;
                break;
            case DialogTreeType.WhoWouldYouBringForDinner:
                trees = dialogSet.WhoWouldYouBringForDinner;
                break;
            default:
                Debug.LogError("Unknown tree type: " + treeType);
                return null;
        }

        var index = Random.Range(0, trees.Length);
        var tree = trees[index];

        Debug.Assert(tree != null, $"Dialog tree {treeType} @ index {index} was null!");
        Debug.Assert(tree.Tree != null, $"Dialog tree entries {treeType} @ index {index} was null!");

        return tree;
    }
}