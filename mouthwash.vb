'========================================================================
' This conversion was produced by the Free Edition of
' Java to VB Converter courtesy of Tangible Software Solutions.
' Order the Premium Edition at https://www.tangiblesoftwaresolutions.com
'========================================================================

Imports System
Imports System.Threading
Imports Microsoft.VisualBasic

Namespace mouthwash

	''' 
	''' <summary>
	''' @author MARTIN
	''' </summary>
	Public Class AntiCurse
		Implements ThreadStart

		Private text As String
		Private parsedText As String
		Private ReadOnly badWords() As String
		Private ReadOnly correctiveSymbol As String

		''' 
		''' <param name="text"> </param>
		Public Sub New(ByVal text As String)
			Me.text = text
			Me.parsedText = text
			Me.badWords = New String(){"bad", "martin", "okay"}
			Me.correctiveSymbol = "*****"
		End Sub

		''' 
		''' <param name="text"> </param>
		''' <param name="listOfBadWord"> </param>
		Public Sub New(ByVal text As String, ParamArray ByVal listOfBadWord() As String)
			Me.text = text
			Me.parsedText = text
			badWords = listOfBadWord
			correctiveSymbol = "*****"
		End Sub

		''' 
		Public Overrides Sub run()

			For i As Integer = 0 To badWords.Length - 1
				Dim currentBadWord As String = badWords(i)

				'Phase 1: Replace all bad words with exact match
				parsedText = parsedText.replaceAll(currentBadWord, correctiveSymbol)

				'Phase 2: Find Exaggerated bad words. for instance 'Bad' --> 'Baaaaadd'
				Dim searchingIndex As Integer = 0
				Do While searchingIndex < parsedText.Length

					Dim firstLetterOfCurrentBadWord As String = currentBadWord.Chars(0).ToString().ToLower()

					'Get index of first letter
					Dim indexFirstLetter As Integer = parsedText.ToLower().IndexOf(" " & firstLetterOfCurrentBadWord, searchingIndex, StringComparison.Ordinal)

					'Get index of last letter
					If indexFirstLetter <> -1 Then
						Dim indexLastLetter1 As Integer = parsedText.ToLower().IndexOf(" ", indexFirstLetter + 1, StringComparison.Ordinal)
						Dim indexLastLetter2 As Integer = parsedText.ToLower().IndexOf(vbTab, indexFirstLetter + 1, StringComparison.Ordinal)
						Dim indexLastLetter3 As Integer = parsedText.ToLower().IndexOf(vbLf, indexFirstLetter + 1, StringComparison.Ordinal)
						Dim indexLastLetter As Integer = parsedText.ToLower().Length - 1

						If indexLastLetter1 <> -1 Then
							indexLastLetter = indexLastLetter1
						ElseIf indexLastLetter2 <> -1 Then
							indexLastLetter = indexLastLetter2
						ElseIf indexLastLetter3 <> -1 Then
							indexLastLetter = indexLastLetter3
						End If

						'get potential bad word
						Dim word As String = parsedText.Substring(indexFirstLetter + 1, indexLastLetter - (indexFirstLetter + 1))

						'following sequence of letters to uncover bad word
						Dim indexOfCurrentWordLetter As Integer = 0
						Dim wordIsBad As Boolean = True
						For j As Integer = 0 To word.Length - 1

							If Not (currentBadWord.Chars(indexOfCurrentWordLetter).ToString().ToLower().Equals(word.Chars(j).ToString().ToLower())) Then

								'if the current character is not a letter from the alphabet
								If Not letterIsInAlphabet(word.ToLower().Chars(j)) Then
									Continue For
								End If

								'look at the next letter is continuing the bad word sequence
								If indexOfCurrentWordLetter < currentBadWord.Length - 1 Then
									If currentBadWord.Chars(indexOfCurrentWordLetter + 1).ToString().ToLower().Equals(word.Chars(j).ToString().ToLower()) Then
										'make loop repeat with different index
										indexOfCurrentWordLetter += 1
										j -= 1
									Else
										wordIsBad = False
									End If
								Else
									wordIsBad = False
								End If

							Else
								If j = word.Length - 1 Then
									'if the letter of the last word matches the last letter of the criteria
									If Not word.Chars(j).ToString().ToLower().Equals(currentBadWord.Chars(currentBadWord.Length - 1).ToString().ToLower()) Then
										wordIsBad = False
									End If
								End If
							End If
						Next j

						'replace word if it is bad
						If wordIsBad Then

							parsedText = parsedText.replaceAll(word, correctiveSymbol)
							'reset loop
							searchingIndex = -1
						End If

					End If

					'continue loop
					searchingIndex += 1
				Loop
			Next i
		End Sub

		''' 
		''' <param name="letter">
		''' @return </param>
		Private Function letterIsInAlphabet(ByVal letter As Char) As Boolean

			Dim alphabet() As Char = {"a"c, "b"c, "c"c, "d"c, "e"c, "f"c, "g"c, "h"c, "i"c, "j"c, "k"c, "l"c, "m"c, "n"c, "o"c, "p"c, "q"c, "r"c, "s"c, "t"c, "u"c, "v"c, "w"c, "x"c, "y"c, "z"c}

			Dim exist As Boolean = False
			For i As Integer = 0 To alphabet.Length - 1
				If alphabet(i) = letter Then
					exist = True
				End If
			Next i

			Return exist
		End Function

		Public Overridable Property Text As String
			Set(ByVal text As String)
				Me.text = text
				Me.parsedText = text
			End Set
			Get
				Return text
			End Get
		End Property


		Public Overridable Property ParsedText As String
			Get
				Return parsedText
			End Get
		End Property
	End Class

End Namespace