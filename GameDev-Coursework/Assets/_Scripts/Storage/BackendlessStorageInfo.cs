public static class BackendlessStorageInfo
{

	public static string ConstructObjectRetrievalURL(string tableName, string objectID)
    {
		return $"https://eu-api.backendless.com/{APPLICATION_ID}/data/{tableName}/{objectID}";
	}

	//var url = "https://eu-api.backendless.com/BDF5EEE0-16C7-5A29-FF91-EDF335C23900/347BF7C1-C3BB-47EF-A1C4-7D60C88CCD6B/data/HighScore";

	public static string APPLICATION_ID = "BDF5EEE0-16C7-5A29-FF91-EDF335C23900"; // you need to add your OWN id here!!
	public static string REST_SECRET_KEY = "347BF7C1-C3BB-47EF-A1C4-7D60C88CCD6B"; // you need to add your OWN key here!!

	// High score table
	public static string HIGH_SCORE_TABLE_NAME = "HighScore";
	public static string HIGH_SCORE_ID = "00C80F61-E44C-44E4-BF80-8201C294409C";

	// Network error message
	public static string NETWORK_ERROR_MESSAGE = "Network error";
}
