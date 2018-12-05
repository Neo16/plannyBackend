using PlannyBackend.BLL.Dtos.Profile;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PlannyBackend.BLL.Interfaces
{
    public interface IProfileService
    {
        Task<ProfileDto> Get(int id);

        Task<EditProfileDto> GetForEdit(int id);

        Task Edit(int id, EditProfileDto profile);
    }
}
