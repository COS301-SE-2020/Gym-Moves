using GymMovesWebAPI.Data.Models.VerificationDatabaseModels;
using System.Threading.Tasks;

namespace GymMovesWebAPI.Data.Repositories.Interfaces {
    public interface IGymMemberRepository {
        public Task<bool> addList(GymMember[] memberList);
        public Task<bool> addMember(GymMember member);
        public Task<GymMember> getMember(string membershipId, int gymId);
    }
}
