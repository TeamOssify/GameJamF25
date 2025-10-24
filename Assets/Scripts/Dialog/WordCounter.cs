public static class WordCounter {
    public static int CountWords(string message) {
        var c = 1;

        for (var i = 0; i < message.Length; i++) {
            if (char.IsWhiteSpace(message[i])) {
                c++;
            }
        }

        return c;
    }

    public static int CountPunctuation(string message) {
        var c = 0;

        for (var i = 0; i < message.Length; i++) {
            if (char.IsPunctuation(message[i])) {
                c++;
            }
        }

        return c;
    }
}