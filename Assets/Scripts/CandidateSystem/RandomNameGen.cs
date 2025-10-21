using UnityEngine;

public static class RandomNameGen {
    private static readonly string[] firstNamePool = new string[] {
        "James",
        "Justin",
        "Kian",
        "Ali"
    };

    private static readonly string[] lastNamePool = new string[] {
        "Payne",
        "Dakin",
        "Cloutier",
        "Sheppard"
    };

    public static string GetRandomFirstName() {
        return firstNamePool[Random.Range(0, firstNamePool.Length)];
    }

    public static string GetRandomLastName() {
        return firstNamePool[Random.Range(0, lastNamePool.Length)];
    }


}
