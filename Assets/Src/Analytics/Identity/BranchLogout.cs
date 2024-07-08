namespace Src.Analytics.Identity
{
    public class BranchLogout : IBranchLogout
    {
        public void Logout()
        {
            Branch.logout();
        }
    }
}