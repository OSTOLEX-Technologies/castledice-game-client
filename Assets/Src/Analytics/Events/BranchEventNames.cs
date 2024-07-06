namespace Src.Analytics.Events
{
    public static class BranchEventNames
    {
        #region Login
        public static readonly BranchEventName Login = new()
        {
            Name = "Login Complete",
            Alias = "login_complete"
        };
        public static readonly BranchEventName Logout = new()
        {
            Name = "Logout",
            Alias = "logout"
        };
        #endregion

        #region Tutorial
        public static readonly BranchEventName NextTutorialStep = new()
        {
            Name = "Tutorial Step Started",
            Alias = "tutorial_step_started"
        };
        #endregion

        #region General
        public static readonly BranchEventName ButtonClick = new()
        {
            Name = "Button Clicked",
            Alias = "button_clicked"
        };
        public static readonly BranchEventName CheckBoxValueChange = new()
        {
            Name = "CheckBox Value Changed",
            Alias = "check_box_value_changed"
        };
        #endregion

        #region InGame
        public static readonly BranchEventName ActionPointsAmountChange = new()
        {
            Name = "Action Points Amount Change",
            Alias = "action_points_amount_change"
        };
        public static readonly BranchEventName InGameAction = new()
        {
            Name = "In-Game Action Fired",
            Alias = "in_game_action"
        };
        #endregion
    }
}