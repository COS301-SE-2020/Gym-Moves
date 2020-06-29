using GymMovesWebAPI.Data.DatabaseContexts.MainDatabaseContext;
using GymMovesWebAPI.Data.Models.VerificationDatabaseModels;
using GymMovesWebAPI.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace GymMovesWebAPI.Data.Repositories.Implementations {
    public class GymMemberRepository : IGymMemberRepository {
        private readonly MainDatabaseContext context;

        public GymMemberRepository(MainDatabaseContext context) {
            this.context = context;
        }

        public async Task<bool> addList(GymMember[] memberList) {
            context.Add(memberList);
            return (await context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> addMember(GymMember member) {
            context.Add(member);
            return (await context.SaveChangesAsync()) > 0;
        }

        public async Task<GymMember> getMember(string membershipId, int gymId) {
            IQueryable<GymMember> query = context.GymMembers;
            query = query.Where(p => p.MembershipId == membershipId);
            query = query.Where(p => p.GymId == gymId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<bool> deleteMember(GymMember member) {
            context.Remove(member);
            return (await context.SaveChangesAsync()) > 0;
        }
    }
}
