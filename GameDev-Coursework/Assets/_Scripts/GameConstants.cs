public static class GameConstants
{
    public static string NPC_TAG = "NPC";
    public static string FPSCONTROLLER = "FPSController";
    public static string PLAYER_TAG = "Player";
    public static string BULLET_TAG = "bullet";

    public static string RED_TEAM_SCORE_TEXT_TAG = "redTeamScoreText";
    public static string BLUE_TEAM_SCORE_TEXT_TAG = "blueTeamScoreText";
    public static string RED_TEAM_SCORE_BAR_TAG = "redTeamScoreBar";
    public static string BLUE_TEAM_SCORE_BAR_TAG = "blueTeamScoreBar";

    public static string RED_TEAM_SPAWN_POINT= "redTeamSpawnPoint";
    public static string BLUE_TEAM_SPAWN_POINT= "blueTeamSpawnPoint";

    public static string RED_TEAM_GOAL = "redTeamGoal"; // Red should defend this
    public static string BLUE_TEAM_GOAL = "blueTeamGoal"; // Blue should defend this

    public static int SCORE_LIMIT = 100;

    // For storage unique names
    public static int NPC_COUNTER = 0;

    // Team spawn numbers
    public static int RED_TEAM_COUNT = 6;
    public static int BLUE_TEA_COUNT = 6;

    // Resources
    public static string PREFAB_FOLDER_PREFIX = "Prefabs/";
    public static string PLAYER_PREFAB_SUFFIX = "FPSController";
    public static string NPC_PREFAB_SUFFIX = "aj_Variant";
    //public static string NPC_PREFAB_SUFFIX = "FixRotationNPC";

    // Spawn look at
    public static string BLUE_TEAM_ON_SPAWN_LOOK_AT = "blueTeamOnSpawnLookAt";
    public static string RED_TEAM_ON_SPAWN_LOOK_AT = "redTeamOnSpawnLookAt";

    // End game scene game object names
    public static string PLAYER_SCORE_TEXT_OBJECT = "PlayerScoreText";
    public static string BLUE_TEAM_SCORE_TEXT_OBJECT = "BlueTeamScore";
    public static string RED_TEAM_SCORE_Text_OBJECT = "RedTeamScore";
    public static string WINNING_TEAM_TEXT_OBJECT = "WinningTeamText";

    // Colors
    public static string RED_TEAM_COLOR = "76000F";
    public static string BLUE_TEAM_COLOR = "5383FF";
    public static string EDGE_ORANGE = "F39361";
}
