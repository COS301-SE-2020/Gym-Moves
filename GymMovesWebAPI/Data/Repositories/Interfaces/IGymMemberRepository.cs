using GymMovesWebAPI.Data.Models.VerificationDatabaseModels;
using GymMovesWebAPI.Data.Models.DatabaseModels;
using System.Threading.Tasks;
using GymMovesWebAPI.Models.DatabaseModels;

namespace GymMovesWebAPI.Data.Repositories.Interfaces {
    public interface IGymMemberRepository {
        public Task<bool> addList(GymMember[] memberList);
        public Task<bool> addMember(GymMember member);
        public Task<GymMember> getMember(string membershipId, int gymId);
        public Task<bool> deleteMember(GymMember member);

    }
}
