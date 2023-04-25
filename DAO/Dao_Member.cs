using GiveAID.Models;

namespace GiveAID.DAO
{
	public class Dao_Member
	{
		private static Dao_Member instance = null;
		private static AIDContext ct;

		private Dao_Member() 
		{
			ct = new();
		}

		public static Dao_Member Instance()
		{
			if (instance == null)
			{
				instance = new Dao_Member();
			}
			return instance;
		}

		public Member Check_Uname_Pass(string name, string pass) 
		{
			var member = (from m in ct.Members
						  where m.mem_username == name && m.mem_password == pass
						  select new Member
						  {
							  mem_id = m.mem_id,
							  mem_username = m.mem_username,
							  mem_password = m.mem_password,
							  mem_name = m.mem_name,
							  gender = m.gender,
							  email = m.email,
							  phone_number = m.phone_number,
							  created_at = m.created_at
						  }).FirstOrDefault();
			return member;
		}
	}
}
