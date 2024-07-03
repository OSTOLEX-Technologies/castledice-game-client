namespace Src.Analytics
{
    public class BranchLogout : IBranchLogout
    {
        public void Logout()
        {
            Branch.logout();
        }
    }
}