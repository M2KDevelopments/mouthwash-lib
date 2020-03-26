#
#     * author: Martin Kululanga
#     * date      : 28 Aug 2018

#     How to use
#     .settext( ' ' ) -> void function
#     .censor() -> void function
#     .getparsedtext() -> returns text
#

class MouthWash:
    # public variables
    __text = None
    __parsedtext = None
    __correctivesymbol = None
    __badwords = []

    # constructor
    def __init__(self):
        self.__badwords = ["bad","words","okay"]
        self.__correctivesymbol = "*****"

    # function that compares a character to the English alphabet
    def __letterisinalphabet(self, letter):
        alphabet = ['a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't','u','v', 'w', 'x', 'y', 'z']
        exist = False
        for i in range(0, len(alphabet)):
            if alphabet[i] == letter:
                exist = True
        return exist

    # censor sentence
    def censor(self):
        for i in range(0, len(self.__badwords)):
            current_bad_word = self.__badwords[i]

            # Phase 1: Replace all bad words with exact match.
            self.__parsedtext = self.__parsedtext.replace(current_bad_word, self.__correctivesymbol)

            # Phase 2: Find Exaggerated bad words. for instance 'Bad' --> 'Baaaaadd'
            searching_index = 0
            while searching_index < len(self.__parsedtext):

                first_letter_of_current_bad_word = current_bad_word.lower()
                first_letter_of_current_bad_word = first_letter_of_current_bad_word[0]

                # Get index of first letter
                index_first_letter = self.__parsedtext.lower().find(" " + first_letter_of_current_bad_word,searching_index)

                # Get index of last letter
                if index_first_letter != -1:
                    index_last_letter1 = self.__parsedtext.lower().find(" ", index_first_letter + 1)
                    index_last_letter2 = self.__parsedtext.lower().find("\t", index_first_letter + 1)
                    index_last_letter3 = self.__parsedtext.lower().find("\n", index_first_letter + 1)
                    index_last_letter = len(self.__parsedtext) - 1

                    if index_last_letter1 != -1:
                        index_last_letter = index_last_letter1
                    elif index_last_letter2 != -1:
                        index_last_letter = index_last_letter2
                    elif index_last_letter3 != -1:
                        index_last_letter = index_last_letter3

                    # get potential bad word
                    word = self.__parsedtext[index_first_letter + 1: index_last_letter]

                    # following sequence of letters to uncover bad word
                    index_of_current_word_letter = 0
                    word_is_bad = True

                    for j in range(0, len(word)):
                        # make words to compare lower case for easier comparison
                        current_bad_word = current_bad_word.lower()
                        word = word.lower()

                        # get letters
                        w1 = current_bad_word[index_of_current_word_letter]
                        w2 = word[j]

                        if w1 != w2:

                            # if the current character is not a letter from the alphabet
                            letter_is_in_the_alphabet = self.__letterisinalphabet(word[j])

                            if letter_is_in_the_alphabet == False:
                                continue

                            # look at the next letter is continuing the bad word sequence
                            if index_of_current_word_letter < len(current_bad_word) - 1:
                                if current_bad_word[index_of_current_word_letter + 1] == word[j]:
                                    # make loop repeat with different index
                                    index_of_current_word_letter += 1
                                    j -= 1
                                else:
                                    word_is_bad = False
                            else:
                                word_is_bad = False
                        else:
                            if j == len(word) - 1:
                                # if the letter of the last word matches the last letter of the criteria
                                if word[j] != current_bad_word[len(current_bad_word) - 1]:
                                    word_is_bad = False

                    # replace word if it is bad
                    if word_is_bad:
                        self.__parsedtext = self.__parsedtext.replace(word, self.__correctivesymbol)
                        # reset loop
                        searching_index = -1

                # continue loop
                searching_index += 1

    #  It sets the text to be censered
    def settext(self, mtext):
        self.__text = mtext
        self.__parsedtext = mtext

    def getparsedtext(self):
        return self.__parsedtext

    def cursedwordexists(self):
        return self.__text.lower() == self.__parsedtext.lower()