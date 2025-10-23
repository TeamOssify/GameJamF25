using UnityEngine;

public static class RandomNameGen {
    private static readonly string[] FirstNamePool = {
        "Gurt",
        "Chuck",
        "Kian",
        "Duke"
    };

    private static readonly string[] LastNamePool = {
        "Gurting",
        "E. Cheese",
        "Cloutier",
        "Von Agnes"
    };

    public static string GetRandomFirstName() {
        return FirstNamePool[Random.Range(0, FirstNamePool.Length)];
    }

    public static string GetRandomLastName() {
        return LastNamePool[Random.Range(0, LastNamePool.Length)];
    }
}