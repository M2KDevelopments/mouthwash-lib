/*
    * author: Martin Kululanga
    * date  : 03 Sep 2018
    
    How to use
    .setText( ' ' ) -> void function
    .censor() -> void function removes all the bad worlds
    .getParsedText() -> returns corrective text
*/



//public variables
var text;
var parsedText;
var badWords = ["bad","words","okay"];
var correctiveSymbol = "*****";

//function that compares a character to the English alphabet
function letterIsInAlphabet(letter) {
 
    var alphabet = ['a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j',
        'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't',
        'u', 'v', 'w', 'x', 'y', 'z'];
 
    var exist = false;
    for (var i = 0; i < alphabet.length; i++) {
        if (alphabet[i] == letter) {
            exist = true;
        }
    }
    return exist; 
}

//censor sentence
function censor(){
        for (var i = 0; i < badWords.length; i++) {
            var currentBadWord = badWords[i];

            //Phase 1: Replace all bad words with exact match
            parsedText = replaceAll(parsedText, currentBadWord, correctiveSymbol);


            //Phase 2: Find Exaggerated bad words. for instance 'Bad' --> 'Baaaaadd'
            var searchingIndex = 0;
            while (searchingIndex < parsedText.length) {
 
                var firstLetterOfCurrentBadWord = currentBadWord.toLowerCase();
                firstLetterOfCurrentBadWord = firstLetterOfCurrentBadWord.charAt(0);

                //Get index of first letter
                var indexFirstLetter = indexOf(parsedText.toLowerCase(), " " + firstLetterOfCurrentBadWord, searchingIndex);

                //Get index of last letter
                if (indexFirstLetter !== -1) {
                    var indexLastLetter1 = indexOf(parsedText.toLowerCase()," ", indexFirstLetter + 1);
                    var indexLastLetter2 = indexOf(parsedText.toLowerCase(),"\t", indexFirstLetter + 1);
                    var indexLastLetter3 = indexOf(parsedText.toLowerCase(),"\n", indexFirstLetter + 1);
                    var indexLastLetter = parsedText.toLowerCase().length - 1;

                    if (indexLastLetter1 != -1) {
                        indexLastLetter = indexLastLetter1;
                    } else if (indexLastLetter2 != -1) {
                        indexLastLetter = indexLastLetter2;
                    } else if (indexLastLetter3 != -1) {
                        indexLastLetter = indexLastLetter3;
                    }

                    //get potential bad word
                    var word = parsedText.substring(indexFirstLetter + 1, indexLastLetter);

                    //following sequence of letters to uncover bad word
                    var indexOfCurrentWordLetter = 0;
                    var wordIsBad = true;
                    for (var j = 0; j < word.length; j++) {
                        
                        //make words to compare lower case for easier comparison
                        currentBadWord = currentBadWord.toLowerCase();
                        word = word.toLowerCase();

                        //get letters
                        var w1 = currentBadWord.charAt(indexOfCurrentWordLetter);
                        var w2 = word.charAt(j);

                        if (w1 != w2) {

                            //if the current character is not a letter from the alphabet
                            var letter_is_in_the_alphabet = letterIsInAlphabet(word.charAt(j));

                            if (letter_is_in_the_alphabet == false) {
                                continue;
                            }
                            
                            //look at the next letter is continuing the bad word sequence
                            if (indexOfCurrentWordLetter < currentBadWord.length - 1) {
                                if (currentBadWord.charAt(indexOfCurrentWordLetter + 1) == word.charAt(j)) {
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
                            if (j == word.length - 1) {
                                //if the letter of the last word matches the last letter of the criteria
                                if ( word.charAt(j) != currentBadWord.charAt(currentBadWord.length - 1) ) {
                                    wordIsBad = false;
                                }
                            }
                        }
                    }

                    //replace word if it is bad
                    if (wordIsBad) {

                        parsedText = replaceAll(parsedText, word, correctiveSymbol);
                        //reset loop
                        searchingIndex = -1;
                    }

                }

                //continue loop
                searchingIndex++;
            }
        }  
}



//it sets the text to be censered
function setText(mtext){
    text = mtext+."";
    parsedText = text;
}


function getParsedText(){
    return parsedText;
}

function cursedWordExists(){
    return (text.toLowerCase() == parsedText.toLowerCase());
}

/*
    ************************************
    Extra string manipulation functions
    ************************************
*/
function replaceAll(phrase,word,replacement){ 
    while(phrase.indexOf(word) >= 0){
        phrase = phrase.replace(word,replacement);
    }
    return phrase;
}

function indexOf(phrase, txt,fromIndex){ 
    phrase = phrase.substring(fromIndex,phrase.length);
    if (phrase.indexOf(txt) >=0){
        return phrase.indexOf(txt) + fromIndex;
    }
    return -1;
}