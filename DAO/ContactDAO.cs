using GiveAID.Models;

namespace GiveAID.DAO
{
    internal class ContactDAO
    {
        #region singleton
        private static ContactDAO? instance = null;
        private ContactDAO() { var context = new AIDContext(); }
        internal static ContactDAO Instance()
        {
            if (instance == null)
            {
                instance = new ContactDAO();
            }
            return instance;
        }
        #endregion
        public void SendContact (string subject, string body, string email, string phone_number, Int16 status, DateTime created_at)
        {
            try
            {
                Contact contact = new Contact();
                contact.email = email;
                contact.phone_number = phone_number;
                contact.subject = subject;
                contact.body = body;

                AIDContext context = new AIDContext();
                context.Contacts?.Add(contact);
                context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
