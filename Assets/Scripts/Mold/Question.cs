using System;

public struct QuestionKeeper
{
	public static string S_QuestionText;
	public static Options S_Options;
	public static int S_QuestionLevel;
	public static string S_QuestionLanguage;
	public static string S_QuestionCategory;
	public static string S_QuestionID;
	public static string S_SenderPlayerID;
}

[Serializable]
public struct Question
{
	public string QuestionText;
	public Options Options;
	public int QuestionLevel;
	public string QuestionLanguage;
	public string QuestionCategory;
	public string QuestionID;
	public string SenderPlayerID;
}

[Serializable]
public struct Options
{
	public Option CorrectOption;
	public Option WrongOption1;
	public Option WrongOption2;
	public Option WrongOption3;
}

[Serializable]
public struct Option
{
	public string OptionText;
	public bool IsCorrectOption;
}