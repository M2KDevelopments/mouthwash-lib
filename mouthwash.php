<?php
/*
    * author: Martin Kululanga
    * date  : 03 Sep 2018
    
    How to use
    .setText( ' ' ) -> void function
    .censor() -> void function removes all the bad worlds
    .getParsedText() -> returns corrective text
*/
	//public variables
	$text;
	$parsedText;
	$badWords = array("bad","words","okay");
	$correctiveSymbol = "*****";

	//function that compares a character to the English alphabet
	function letterIsInAlphabet($letter) {
	 
	    $alphabet = array('a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j',
	    	        'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't',
	    	        'u', 'v', 'w', 'x', 'y', 'z');
	 
	    $exist = false;
	    for ($i = 0; $i < count($alphabet); $i++) {
	        if ($alphabet[$i] == $letter) {
	            $exist = true;
	        }
	    }
	    return $exist; 
	}

	//censor sentence
	function censor(){

		//declare that you are using the global variables
		global $text;
		global $parsedText;
		global $badWords;
		global $correctiveSymbol;

		for ($i = 0; $i < count($badWords); $i++) {
            $currentBadWord = $badWords[$i];

            //Phase 1: Replace all bad words with exact match
            $parsedText = str_replace($currentBadWord, $correctiveSymbol,$parsedText);

            //Phase 2: Find Exaggerated bad words. for instance 'Bad' --> 'Baaaaadd'
            $searchingIndex = 0;

            while ($searchingIndex < strlen($parsedText)) {

                $firstLetterOfCurrentBadWord = strtolower($currentBadWord){0};
 
                //Get index of first letter
                $indexFirstLetter = indexOf(strtolower($parsedText), " " . $firstLetterOfCurrentBadWord, $searchingIndex);

                //Get index of last letter
                if ($indexFirstLetter !== -1) {
                    $indexLastLetter1 = indexOf(strtolower($parsedText)," ", $indexFirstLetter + 1);
                    $indexLastLetter2 = indexOf(strtolower($parsedText),"\t", $indexFirstLetter + 1);
                    $indexLastLetter3 = indexOf(strtolower($parsedText),"\n", $indexFirstLetter + 1);
                    $indexLastLetter = strlen($parsedText) - 1;

                    if ($indexLastLetter1 != -1) {
                        $indexLastLetter = $indexLastLetter1;
                    } else if ($indexLastLetter2 != -1) {
                        $indexLastLetter = $indexLastLetter2;
                    } else if ($indexLastLetter3 != -1) {
                        $indexLastLetter = $indexLastLetter3;
                    }

                    $word_length = (($indexLastLetter)- ($indexFirstLetter + 1));
                    $word = substr($parsedText, $indexFirstLetter + 1, $word_length);
  

                    //following sequence of letters to uncover bad word
                    $indexOfCurrentWordLetter = 0;
                    $wordIsBad = true;
                    for ($j = 0; $j < strlen($word); $j++) {
                        
                        //make words to compare lower case for easier comparison
                        $currentBadWord = strtolower($currentBadWord);
                        $word = strtolower($word);

                        //get letters
                        $w1 = $currentBadWord{$indexOfCurrentWordLetter};
                        $w2 = $word{$j};

                        if ($w1 != $w2) {

                            //if the current character is not a letter from the alphabet
                            $letter_is_in_the_alphabet = letterIsInAlphabet($word{$j});

                            if ($letter_is_in_the_alphabet == false) {
                                continue;
                            }
                            
                            //look at the next letter is continuing the bad word sequence
                            if ($indexOfCurrentWordLetter < strlen($currentBadWord) - 1) {
                                if ($currentBadWord{$indexOfCurrentWordLetter + 1} == $word{$j}) {
                                    //make loop repeat with different index
                                    $indexOfCurrentWordLetter++;
                                    $j--;
                                } else {
                                     
                                    $wordIsBad = false;
                                }
                            } else {
                                 
                                $wordIsBad = false;
                            }

                        } else {
                            if ($j == strlen($word) - 1) {
                                //if the letter of the last word matches the last letter of the criteria
                                if ( $word{$j} != $currentBadWord{strlen($currentBadWord) - 1} ) {
                                     
                                    $wordIsBad = false;
                                }
                            }
                        }
                    }

                    //replace word if it is bad
                    if ($wordIsBad) {

                        $parsedText = substr_replace($parsedText, $correctiveSymbol,($indexFirstLetter+1), $word_length);

                        //reset loop
                        $searchingIndex = -1;
                    }
                }

                //continue loop
                $searchingIndex++;
                echo $searchingIndex . "<br>";
            }
        }        
	}

	//it sets the text to be censered
	function setText($mtext){
	    //declare that you are using the global variables
	    global $text;
	    global $parsedText; 
	    $text = $mtext . "";
	    $parsedText = $text;
	}

	function getParsedText(){
		//declare that you are using the global variables
		global $parsedText;
	    return $parsedText;
	}

	function cursedWordExists(){
	    return (strtolower($text) == strtolower($parsedText));
	}

		/*
	    ************************************
	    Extra string manipulation functions
	    ************************************
	
    */
	function indexOf($phrase, $txt, $fromIndex){ 
	    $phrase = substr($phrase,$fromIndex,strlen($phrase));
	    $index = strpos(strtolower($phrase),strtolower($txt));
	    if ($index == ""){
	        return -1;
	    }
	    return $index + $fromIndex;
	}	
?>