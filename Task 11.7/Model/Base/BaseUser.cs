namespace Task_11._7.Model.Base
{
    public abstract class BaseUser
    {
        public abstract void ReadInfoClients(long phone);

        public abstract void SetPhoneNumber(long phone);

        public abstract string ChangeInfoClient(string whatChanged, string typeOfChanged);

        public abstract void SaveChangedInfo(string changedInfo);

        public abstract string GetClientInfo(long phone);

    }
}
