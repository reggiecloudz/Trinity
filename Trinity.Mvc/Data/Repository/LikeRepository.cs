using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trinity.Mvc.Domain;

namespace Trinity.Mvc.Data.Repository
{
    public class LikeRepository : ILikeRepository
    {
        private readonly ApplicationDbContext _context;

        public LikeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool LikeExist(long objectId, LikableType type, string userId)
        {
            var like = _context.Likes.Where(l => l.ObjectId == objectId && l.Type == type && l.UserId == userId).Any();

            return like;
        }
    }
}