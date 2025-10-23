using UnityEngine;

public static class RandomNameGen {
    private static readonly string[] firstNamePool = new string[] {
        "Gurt",
        "Chuck",
        "Kian",
        "Duke"
    };

    private static readonly string[] lastNamePool = new string[] {
        "Gurting",
        "E. Cheese",
        "Cloutier",
        "Von Agnes"
    };

    public static string GetRandomFirstName() {
        return firstNamePool[Random.Range(0, firstNamePool.Length)];
    }

    public static string GetRandomLastName() {
        return lastNamePool[Random.Range(0, lastNamePool.Length)];
    }


}
