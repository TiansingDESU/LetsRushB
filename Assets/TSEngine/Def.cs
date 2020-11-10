namespace Assets.Def
{

     public static class SceneDef
     {
        public static string MainScene = "MainScene";
        public static string TestNetScene = "TestNetLevel";
     }

    public static class UIDef
    {
        public static string UIRoot = "Main/UIRoot";
        public static string UI_Main = "Main_UI";
        public static string UI_Pop = "Pop_UI";
        public static string UI_Credits = "Credits_UI";
        public static string UI_Connect = "Connect_UI";
        public static string UI_Lobby = "Lobby_UI";
        public static string UI_Room = "Room_UI";
    }

    public static class RaiseEventCode
    {
        public static byte LEVEL_LOAD_END_EVENT = 1;
    }
}
