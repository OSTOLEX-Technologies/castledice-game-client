namespace Src.Analytics.Identity
{
    public class BranchLogin : IBranchLogin
    {
        public void Login(string userId)
        {
            Branch.setIdentity(userId);
        }
    }
}