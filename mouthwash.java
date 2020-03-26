/*
    @author: Martin Kululanga
    * date      : 28 Aug 2018
    
    How to use
    .setText( ' ' ) -> void function
    .censor() -> void function
    .getParsedText() -> returns text
*/
public class MouthWash implements Runnable {

    private String text;
    private String parsedText;
    private final String badWords[];
    private final String correctiveSymbol;

    /**
     *
     * @param text
     */
    public MouthWash(String text) {
        this.text = text;
        this.parsedText = text;
        this.badWords = new String[]{"bad", "martin", "okay"};
        this.correctiveSymbol = "*****";
    }

    /**
     *
     * @param text
     * @param listOfBadWord
     */
    public MouthWash(String text, String... listOfBadWord) {
        this.text = text;
        this.parsedText = text;
        badWords = listOfBadWord;
        correctiveSymbol = "*****";
    }

    /**
     *
     */
    @Override
    public void run() {

        for (int i = 0; i < badWords.length; i++) {
            String currentBadWord = badWords[i];

            //Phase 1: Replace all bad words with exact match
            parsedText = parsedText.replaceAll(currentBadWord, correctiveSymbol);

            //Phase 2: Find Exaggerated bad words. for instance 'Bad' --> 'Baaaaadd'
            int searchingIndex = 0;
            while (searchingIndex < parsedText.length()) {

                String firstLetterOfCurrentBadWord = String.valueOf(currentBadWord.charAt(0)).toLowerCase();

                //Get index of first letter
                int indexFirstLetter = parsedText.toLowerCase().indexOf(" " + firstLetterOfCurrentBadWord, searchingIndex);

                //Get index of last letter
                if (indexFirstLetter != -1) {
                    int indexLastLetter1 = parsedText.toLowerCase().indexOf(" ", indexFirstLetter + 1);
                    int indexLastLetter2 = parsedText.toLowerCase().indexOf("\t", indexFirstLetter + 1);
                    int indexLastLetter3 = parsedText.toLowerCase().indexOf("\n", indexFirstLetter + 1);
                    int indexLastLetter = parsedText.toLowerCase().length() - 1;

                    if (indexLastLetter1 != -1) {
                        indexLastLetter = indexLastLetter1;
                    } else if (indexLastLetter2 != -1) {
                        indexLastLetter = indexLastLetter2;
                    } else if (indexLastLetter3 != -1) {
                        indexLastLetter = indexLastLetter3;
                    }

                    //get potential bad word
                    String word = parsedText.substring(indexFirstLetter + 1, indexLastLetter);

                    //following sequence of letters to uncover bad word
                    int indexOfCurrentWordLetter = 0;
                    boolean wordIsBad = true;
                    for (int j = 0; j < word.length(); j++) {

                        if (!(String.valueOf(currentBadWord.charAt(indexOfCurrentWordLetter)).toLowerCase().equals(String.valueOf(word.charAt(j)).toLowerCase()))) {

                            //if the current character is not a letter from the alphabet
                            if (!letterIsInAlphabet(word.toLowerCase().charAt(j))) {
                                continue;
                            }

                            //look at the next letter is continuing the bad word sequence
                            if (indexOfCurrentWordLetter < currentBadWord.length() - 1) {
                                if (String.valueOf(currentBadWord.charAt(indexOfCurrentWordLetter + 1)).toLowerCase().equals(String.valueOf(word.charAt(j)).toLowerCase())) {
                                    //make loop repeat with different index
                                    indexOfCurrentWordLetter++;
                                    --j;
                                } else {
                                    wordIsBad = false;
                                }
                            } else {
                                wordIsBad = false;
                            }

                        } else {
                            if (j == word.length() - 1) {
                                //if the letter of the last word matches the last letter of the criteria
                                if (!String.valueOf(word.charAt(j)).toLowerCase().equals(String.valueOf(currentBadWord.charAt(currentBadWord.length() - 1)).toLowerCase())) {
                                    wordIsBad = false;
                                }
                            }
                        }
                    }

                    //replace word if it is bad
                    if (wordIsBad) {

                        parsedText = parsedText.replaceAll(word, correctiveSymbol);
                        //reset loop
                        searchingIndex = -1;
                    }

                }

                //continue loop
                searchingIndex++;
            }
        }
    }

    /**
     *
     * @param letter
     * @return
     */
    private boolean letterIsInAlphabet(char letter) {

        char[] alphabet = new char[]{'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j',
            'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't',
            'u', 'v', 'w', 'x', 'y', 'z'};

        boolean exist = false;
        for (int i = 0; i < alphabet.length; i++) {
            if (alphabet[i] == letter) {
                exist = true;
            }
        }

        return exist;
    }

    public void setText(String text) {
        this.text = text;
        this.parsedText = text;
    }

    public String getText() {
        return text;
    }

    public String getParsedText() {
        return parsedText;
    }
}
