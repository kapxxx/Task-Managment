using DTOs.RequestDTO;
using DTOs.ResponseDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Interface
{
	public interface IUserManager
	{
		public UserDto UserFindByName (LoginDto loginDto);
	}
}
